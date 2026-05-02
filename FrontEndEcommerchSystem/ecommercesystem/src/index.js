import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

import Authprovider from "./Context/AuthProvider";
import { WishlistProvider } from "./Context/WishListContext";
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Authprovider>
        <WishlistProvider>
          <App />
        </WishlistProvider>
      </Authprovider>
    </BrowserRouter>
  </React.StrictMode>,
);
