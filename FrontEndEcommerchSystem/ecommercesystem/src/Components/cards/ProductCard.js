import { Button, Card } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCartArrowDown,
  faHeart,
  faMagnifyingGlass,
} from "@fortawesome/free-solid-svg-icons";
import { useWishlist } from "../../Context/WishListContext";
import "./ProductCard.css";
import useCart from "../hooks/useCart";
import useWishlistApi from "../hooks/useWishlistApi";
export default function ProductCard({
  item,
  conditionConfig,
  forceWishlisted = false,
}) {
  const { wishlist, toggleWishlist } = useWishlist();
  const { addToCart } = useCart();
  const { addtoWhiteList, RemoveWhiteList } = useWishlistApi();

  const isWishlisted = forceWishlisted || wishlist.includes(item.id);
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
            onClick={async () => {
              const isWishlisted = wishlist.includes(item.id);
              try {
                if (isWishlisted) {
                  await RemoveWhiteList(item.id);
                } else {
                  await addtoWhiteList(item.id);
                }
                toggleWishlist(item.id);
              } catch (error) {
                console.log("Failed to update wishlist:", error);
              }
            }}
          >
            <FontAwesomeIcon
              icon={faHeart}
              style={{
                color: isWishlisted ? "red" : "#ccc",
              }}
            />
          </div>
          {/* استبدل الـ Button القديمة بالكود ده */}
          <div
            style={{
              position: "relative",
              width: "130px",
              height: "38px",
              overflow: "hidden",
              borderRadius: "6px",
              cursor: "pointer",
            }}
            className="btn-cart-wrapper"
            onClick={() => {
              addToCart(item.id);
            }}
          >
            {/* النص */}
            <Button className="btn-slide-text">ADD TO CART</Button>

            {/* أيقونة السلة */}
            <div className="btn-slide-icon">
              <FontAwesomeIcon icon={faCartArrowDown} />
            </div>
          </div>
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
