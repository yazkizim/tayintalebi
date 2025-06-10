import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import KendiTaleplerim from "./pages/KendiTaleplerim";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/taleplerim" element={<KendiTaleplerim />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
