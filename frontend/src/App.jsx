import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import SalesOrder from "./pages/SalesOrder";
import Header from "./components/Header";

function App() {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/new-order" element={<SalesOrder />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
