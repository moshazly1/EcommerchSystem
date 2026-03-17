import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

import UserAuthProvider from "./Pages/Context/Context";
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <UserAuthProvider>
        <App />
      </UserAuthProvider>
    </BrowserRouter>
  </React.StrictMode>,
);
