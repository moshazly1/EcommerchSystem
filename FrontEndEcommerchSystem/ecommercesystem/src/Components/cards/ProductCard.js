import { Button, Card } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart, faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import { useWishlist } from "../../Context/WishListContext";

export default function ProductCard({ item, conditionConfig }) {
  const { wishlist, toggleWishlist } = useWishlist();

  return (
    <Card
      className="h-100 shadow-sm"
      style={{ backgroundColor: "var(--brand-main-2)" }}
    >
      {/* Condition Badge (نفس الشكل القديم) */}
      <div
        className="text-white"
        style={{
          background: conditionConfig[item.condition]?.color,
          borderRadius: "6px 0px 0px 0px",
          padding: "2px 6px",
          width: "fit-content",
        }}
      >
        {conditionConfig[item.condition]?.label}
      </div>

      {/* Image (نفس القديم بالظبط) */}
      <div
        className="d-flex justify-content-center align-items-center mx-3 mt-3"
        style={{
          backgroundColor: "#f3f3f4",
          height: "180px",
          flexShrink: 0,
        }}
      >
        <Card.Img
          src={item.mainImageUrl}
          alt={item.name}
          style={{
            objectFit: "contain",
            width: "190px",
            height: "190px",
          }}
        />
      </div>

      <Card.Body>
        {/* Title (نفس الـ clamp) */}
        <Card.Title
          className="fw-bold"
          style={{
            minHeight: "30px",
            overflow: "hidden",
            display: "-webkit-box",
            WebkitLineClamp: 3,
            WebkitBoxOrient: "vertical",
          }}
        >
          {item.name}
        </Card.Title>

        {/* Description (نفس القديم) */}
        <Card.Text
          className="text-secondary"
          style={{
            minHeight: "60px",
            overflow: "hidden",
            display: "-webkit-box",
            WebkitLineClamp: 3,
            WebkitBoxOrient: "vertical",
          }}
        >
          {item.description}
        </Card.Text>

        {/* Price + Actions (نفس الـ layout) */}
        <div className="d-flex align-items-center justify-content-around">
          <Card.Text
            className="fw-bold mt-3"
            style={{ color: "var(--brand-500)" }}
          >
            ${item.price}
          </Card.Text>

          <div
            className="p-2 border rounded"
            style={{ cursor: "pointer" }}
            onClick={() => toggleWishlist(item.id)}
          >
            <FontAwesomeIcon
              icon={faHeart}
              style={{
                color: wishlist.includes(item.id) ? "red" : "#ccc",
              }}
            />
          </div>

          <Button> ADD TO Cart</Button>
        </div>

        {/* Stock + Icon (نفس الشكل 100%) */}
        <div className="d-flex align-items-center justify-content-between p-2">
          <span
            style={{
              background: item.stock < 5 ? "#CC4204" : "#28a745",
              color: "#fff",
              padding: "2px 6px",
              borderRadius: "6px",
              display: "inline-flex",
              alignItems: "center",
              width: "fit-content",
              fontSize: "12px",
            }}
          >
            {item.stock} in stock
          </span>

          <FontAwesomeIcon icon={faMagnifyingGlass} className="fs-5" />
        </div>
      </Card.Body>
    </Card>
  );
}
