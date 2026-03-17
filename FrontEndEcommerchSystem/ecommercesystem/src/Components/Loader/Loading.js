import React from "react";
import "./Loading.css";

export default function Loader() {
  return (
    <div className="overlay">
      <div className="loader">
        {Array.from({ length: 12 }).map((_, i) => (
          <div key={i} className={`bar bar${i + 1}`}></div>
        ))}
      </div>
    </div>
  );
}
