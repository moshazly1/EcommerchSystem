import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

import Authprovider from "./Context/AuthProvider";
import { WishlistProvider } from "./Context/WishListContext";
import { loadStripe } from "@stripe/stripe-js";
import { Elements } from "@stripe/react-stripe-js";
import { CartContext, CartProvider } from "./Context/CartContext";
const stripePromise = loadStripe(
  "pk_test_51TvrpYAWXc2AsGLay9m4bpFtkr6jqRzRrQmAHCCkxVyb8B92iKNIHAuj1t1SLWXdzJGayzU3Q7xLN4nztfmlh21Z00YGCWlZhk",
);
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Authprovider>
        <WishlistProvider>
          <CartProvider>
            <Elements stripe={stripePromise}>
              <App />
            </Elements>
          </CartProvider>
        </WishlistProvider>
      </Authprovider>
    </BrowserRouter>
  </React.StrictMode>,
);
