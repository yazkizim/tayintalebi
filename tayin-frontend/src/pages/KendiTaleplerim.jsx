import React, { useEffect, useState } from "react";
import api from "../services/api";

const KendiTaleplerim = () => {
  const [talepler, setTalepler] = useState([]);

  useEffect(() => {
    api.get("/tayintalebi/benim")
      .then(res => setTalepler(res.data))
      .catch(() => alert("Veriler alınamadı"));
  }, []);

  return (
    <div>
      <h3>Kendi Tayin Taleplerim</h3>
      <table>
        <thead>
          <tr>
            <th>Tür</th>
            <th>İl Tercihi</th>
            <th>Açıklama</th>
            <th>Sonuç</th>
          </tr>
        </thead>
        <tbody>
          {talepler.map(t => (
            <tr key={t.id}>
              <td>{t.talepTuru}</td>
              <td>{t.ilTercihi}</td>
              <td>{t.aciklama}</td>
              <td>{t.sonuc}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default KendiTaleplerim;
