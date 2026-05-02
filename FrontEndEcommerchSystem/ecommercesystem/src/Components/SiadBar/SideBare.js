import { useState } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Stack from "react-bootstrap/Stack";

const Sidebar = ({
  brands = [],
  priceRange = { min: 0, max: 5000 },
  onFilterChange,
}) => {
  const [selectedBrands, setSelectedBrands] = useState([]);
  const [price, setPrice] = useState(priceRange.max);

  const toggleBrand = (brand) => {
    setSelectedBrands((prev) =>
      prev.includes(brand) ? prev.filter((b) => b !== brand) : [...prev, brand],
    );
  };

  const handleApply = () => {
    if (onFilterChange) {
      onFilterChange({ brands: selectedBrands, maxPrice: price });
    }
  };

  return (
    <aside
      style={{
        width: "200px",
        minHeight: "100vh",
        flexShrink: 0,
        borderRight: "2px solid #E2E8F0",
        padding: "24px 16px",
        backgroundColor: "#fff",
      }}
    >
      {/* Header */}
      <Stack gap={0} className="mb-3">
        <span
          style={{
            fontSize: "11px",
            fontWeight: 700,
            letterSpacing: "0.08em",
            color: "#0d0d0d",
          }}
        >
          FILTER PRODUCTS
        </span>
        <span className="text-muted" style={{ fontSize: "11px" }}>
          Precision selection
        </span>
      </Stack>

      <hr className="my-3" />

      {/* Brands */}
      <Stack gap={2} className="mb-3">
        <span
          className="text-muted"
          style={{ fontSize: "10px", fontWeight: 700, letterSpacing: "0.1em" }}
        >
          BRANDS
        </span>
        {brands.map((brand) => (
          <Form.Check
            key={brand}
            type="checkbox"
            id={`brand-${brand}`}
            label={brand}
            checked={selectedBrands.includes(brand)}
            onChange={() => toggleBrand(brand)}
            style={{ fontSize: "13px" }}
          />
        ))}
      </Stack>

      <hr className="my-3" />

      {/* Price Range */}
      <Stack gap={2} className="mb-4">
        <span
          className="text-muted"
          style={{ fontSize: "10px", fontWeight: 700, letterSpacing: "0.1em" }}
        >
          PRICE RANGE
        </span>

        <Form.Range
          min={priceRange.min}
          max={priceRange.max}
          value={price}
          onChange={(e) => setPrice(Number(e.target.value))}
        />

        <div className="d-flex justify-content-between">
          <span className="text-muted" style={{ fontSize: "11px" }}>
            ${priceRange.min.toLocaleString()}
          </span>
          <span className="text-muted" style={{ fontSize: "11px" }}>
            ${price.toLocaleString()}
          </span>
        </div>
      </Stack>

      {/* Apply Button */}
      <Button
        variant="primary"
        className="w-100"
        style={{ fontSize: "11px", fontWeight: 700, letterSpacing: "0.08em" }}
        onClick={handleApply}
      >
        APPLY FILTERS
      </Button>
    </aside>
  );
};

export default Sidebar;
