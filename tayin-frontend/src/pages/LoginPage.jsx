import React, { useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

const LoginPage = () => {
  const [sicilNo, setSicilNo] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const response = await api.post("/auth/login", { sicilNo, password });
      localStorage.setItem("token", response.data.token);
      navigate("/taleplerim");
    } catch (err) {
      alert("Giriş başarısız.");
    }
  };

  return (
    <div className="login">
      <h2>Personel Girişi</h2>
      <input placeholder="Sicil No" value={sicilNo} onChange={e => setSicilNo(e.target.value)} />
      <input placeholder="Şifre" type="password" value={password} onChange={e => setPassword(e.target.value)} />
      <button onClick={handleLogin}>Giriş Yap</button>
    </div>
  );
};

export default LoginPage;
