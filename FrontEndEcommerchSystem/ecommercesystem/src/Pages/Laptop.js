// import { Button, Card, Col, Row } from "react-bootstrap";
// import { useLaptops } from "../Components/hooks/useLaptops";
// import Sidebar from "../Components/SiadBar/SideBare";
// import { faHeart, faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
// import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
// import { useState } from "react";

// export default function Laptop() {
//   const { laptops, error, loading } = useLaptops();
//   const [wishlist, setWishlist] = useState([]);

//   const toggleWishlist = (id) => {
//     setWishlist((prev) =>
//       prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id],
//     );
//   };

//   const conditionConfig = {
//     0: { label: "NEW ARRIVAL", color: "#CC4204" },
//     1: { label: "USED GRADE A", color: "#0000FF" },
//     2: { label: "USED GRADE B", color: "#000000" }, // أسود
//     3: { label: "USED GRADE C", color: "#808080" }, // رمادي
//   };
//   return (
//     <div className="d-flex min-vh-100">
//       <Sidebar />
//       <div
//         className="p-5 flex-grow-1"
//         style={{ backgroundColor: "var(--brand-main)" }}
//       >
//         <h1>Laptops</h1>
//         <p className="text-secondary pb-4">
//           Curated high-performance computing machines for designers,
//           <br /> engineers, and creators.
//         </p>
//         {loading && <p>Loading...</p>}
//         {error && <p>Error loading products</p>}

//         <Row className="g-4">
//           {laptops.map((laptop) => (
//             <Col key={laptop.id} xs={12} sm={6} md={4} lg={3}>
//               <Card
//                 className="h-100 shadow-sm"
//                 style={{ backgroundColor: "var(--brand-main-2)" }}
//               >
//                 <div
//                   className="text-white"
//                   style={{
//                     background: conditionConfig[laptop.condition]?.color,
//                     borderRadius: "6px 0px 0px 0px",
//                     padding: "2px 6px", // قللنا المساحة

//                     width: "fit-content", // يخليها على قد المحتوى
//                   }}
//                 >
//                   {conditionConfig[laptop.condition]?.label}
//                 </div>
//                 <div
//                   className="d-flex justify-content-center align-items-center  mx-3 mt-3  "
//                   style={{
//                     backgroundColor: "#f3f3f4",
//                     height: "180px",
//                     flexShrink: 0,
//                   }}
//                 >
//                   <Card.Img
//                     src={laptop.mainImageUrl}
//                     alt={laptop.name}
//                     style={{
//                       objectFit: "contain",
//                       width: "190px",
//                       height: "190px",
//                     }}
//                   />
//                 </div>

//                 <Card.Body>
//                   <Card.Title
//                     className="fw-bold"
//                     style={{
//                       minHeight: "30px",
//                       overflow: "hidden",
//                       display: "-webkit-box",
//                       WebkitLineClamp: 3,
//                       WebkitBoxOrient: "vertical",
//                     }}
//                   >
//                     {laptop.name}
//                   </Card.Title>
//                   <Card.Text
//                     className="text-secondary"
//                     style={{
//                       minHeight: "60px",
//                       overflow: "hidden",
//                       display: "-webkit-box",
//                       WebkitLineClamp: 3,
//                       WebkitBoxOrient: "vertical",
//                     }}
//                   >
//                     {laptop.description}
//                   </Card.Text>

//                   <div className="d-flex  align-items-center justify-content-around ">
//                     <Card.Text
//                       className="fw-bold mt-3"
//                       style={{ color: "var(--brand-500)" }}
//                     >
//                       ${laptop.price}
//                     </Card.Text>
//                     <div
//                       className="p-2 border rounded"
//                       style={{ cursor: "pointer" }}
//                       onClick={() => toggleWishlist(laptop.id)}
//                     >
//                       <FontAwesomeIcon
//                         icon={faHeart}
//                         style={{
//                           color: wishlist.includes(laptop.id) ? "red" : "#ccc",
//                         }}
//                       />
//                     </div>
//                     <Button>ADD TO Card</Button>
//                   </div>
//                   <div className="d-flex align-items-center justify-content-between p-2">
//                     <span
//                       style={{
//                         background: laptop.stock < 5 ? "#CC4204" : "#28a745",
//                         color: "#fff",
//                         padding: "2px 6px",
//                         borderRadius: "6px",
//                         display: "inline-flex", // أفضل من inline-block
//                         alignItems: "center",
//                         width: "fit-content",
//                         fontSize: "12px",
//                       }}
//                     >
//                       {laptop.stock} in stock
//                     </span>

//                     <FontAwesomeIcon
//                       icon={faMagnifyingGlass}
//                       className="fs-5"
//                     />
//                   </div>
//                 </Card.Body>
//               </Card>
//             </Col>
//           ))}
//         </Row>
//       </div>
//     </div>
//   );
// }
import { Col, Row } from "react-bootstrap";
import Sidebar from "../Components/SiadBar/SideBare";
import ProductCard from "../Components/cards/ProductCard";
import { useLaptops } from "../Components/hooks/useLaptops";

export default function Laptop() {
  const { laptops, loading, error } = useLaptops();

  const conditionConfig = {
    0: { label: "NEW ARRIVAL", color: "#CC4204" },
    1: { label: "USED GRADE A", color: "#0000FF" },
    2: { label: "USED GRADE B", color: "#000000" },
    3: { label: "USED GRADE C", color: "#808080" },
  };

  return (
    <div className="d-flex min-vh-100">
      {/* Sidebar */}
      <Sidebar />

      {/* Main Content */}
      <div
        className="p-5 flex-grow-1"
        style={{ backgroundColor: "var(--brand-main)" }}
      >
        {/* 🔥 نفس الشكل القديم */}
        <h1>Laptops</h1>
        <p className="text-secondary pb-4">
          Curated high-performance computing machines for designers,
          <br /> engineers, and creators.
        </p>

        {loading && <p>Loading...</p>}
        {error && <p>Error loading products</p>}

        {/* Products Grid */}
        <Row className="g-4">
          {laptops.map((laptop) => (
            <Col key={laptop.id} xs={12} sm={6} md={4} lg={3}>
              <ProductCard item={laptop} conditionConfig={conditionConfig} />
            </Col>
          ))}
        </Row>
      </div>
    </div>
  );
}
