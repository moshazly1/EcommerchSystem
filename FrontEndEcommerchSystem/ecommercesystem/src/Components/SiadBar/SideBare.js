import { useEffect, useState } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Stack from "react-bootstrap/Stack";
import { ToggleButton, ToggleButtonGroup } from "react-bootstrap";

import useSideBar from "./useSideBar";

const Sidebar = ({ onFilterChange }) => {
  const [selectedBrands, setSelectedBrands] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [selectedSubCategory, setSelectedSubCategory] = useState(null);

  const [price, setPrice] = useState(0);

  const {
    brands = [],
    error,
    loading,
    category = [],
    SubCategory = [],
    RangePrice,
  } = useSideBar();

  const minPrice = RangePrice?.minPrice ?? 0;
  const maxPrice = RangePrice?.maxPrice ?? 0;

  useEffect(() => {
    if (RangePrice?.maxPrice != null) {
      setPrice(Number(RangePrice.maxPrice));
    }
  }, [RangePrice]);

  const toggleBrand = (brandId) => {
    setSelectedBrands((prev) =>
      prev.includes(brandId)
        ? prev.filter((id) => id !== brandId)
        : [...prev, brandId],
    );
  };

  const handleCategorySelect = (categoryId) => {
    const newCategoryId = selectedCategory === categoryId ? null : categoryId;
    setSelectedCategory(newCategoryId);
    setSelectedSubCategory(null);
  };

  const handleSubCategorySelect = (subCategoryId) => {
    setSelectedSubCategory((prev) =>
      prev === subCategoryId ? null : subCategoryId,
    );
  };

  const filteredSubCategories = selectedCategory
    ? SubCategory.filter((sub) => sub.categoryId === selectedCategory)
    : SubCategory;

  const handleApply = () => {
    const filters = {
      brands: selectedBrands,
      categoryId: selectedCategory,
      subCategoryId: selectedSubCategory,
      maxPrice: price,
    };

    if (onFilterChange) {
      onFilterChange(filters);
    }
  };

  const handleReset = () => {
    setSelectedBrands([]);
    setSelectedCategory(null);
    setSelectedSubCategory(null);
    setPrice(maxPrice);

    if (onFilterChange) {
      onFilterChange({
        brands: [],
        categoryId: null,
        subCategoryId: null,
        maxPrice: maxPrice,
      });
    }
  };

  return (
    <aside
      style={{
        width: "200px",
        flexShrink: 0,
        borderRight: "2px solid #E2E8F0",
        padding: "24px 16px",
        backgroundColor: "#fff",
        minHeight: "100%",
      }}
    >
      <Stack gap={2} className="mb-4">
        <span
          className="text-muted"
          style={{ fontSize: "10px", fontWeight: 700, letterSpacing: "0.1em" }}
        >
          PRICE RANGE
        </span>
        <Form.Range
          min={minPrice}
          max={maxPrice}
          value={price}
          step={0.01}
          disabled={maxPrice === 0}
          onChange={(e) => setPrice(Number(e.target.value))}
        />

        <div className="d-flex justify-content-between">
          <span className="text-muted" style={{ fontSize: "11px" }}>
            ${minPrice.toLocaleString()}
          </span>
          <span
            className="text-muted"
            style={{ fontSize: "11px", fontWeight: 600 }}
          >
            ${Number(price).toLocaleString()}
          </span>
        </div>
      </Stack>

      <hr className="my-3" />

      {/* BRANDS */}
      <Stack gap={2} className="mb-3">
        <span
          className="text-muted d-block mb-2"
          style={{ fontSize: "10px", fontWeight: 700, letterSpacing: "0.1em" }}
        >
          BRANDS
        </span>

        {loading ? (
          <small className="text-muted">Loading brands...</small>
        ) : error ? (
          <small className="text-danger">{error}</small>
        ) : (
          <div className="d-flex" style={{ flexWrap: "wrap", gap: "8px 16px" }}>
            {brands.map((brand) => (
              <Form.Check
                key={brand.id}
                type="checkbox"
                id={`brand-${brand.id}`}
                label={brand.name}
                checked={selectedBrands.includes(brand.id)}
                onChange={() => toggleBrand(brand.id)}
                style={{ fontSize: "13px" }}
              />
            ))}
          </div>
        )}
      </Stack>

      <hr className="my-3" />

      {/* CATEGORY */}
      <span
        className="text-muted d-block mb-2"
        style={{ fontSize: "10px", fontWeight: 700, letterSpacing: "0.1em" }}
      >
        CATEGORY
      </span>

      <ToggleButtonGroup
        type="radio"
        name="category"
        value={selectedCategory}
        onChange={handleCategorySelect}
        className="d-flex flex-wrap"
        style={{ gap: "6px" }}
      >
        {category.map((cat) => {
          const isSelected =
            String(selectedCategory) === String(cat.categoryId);
          return (
            <ToggleButton
              key={cat.categoryId}
              id={`category-${cat.categoryId}`}
              value={cat.categoryId}
              variant={isSelected ? "primary" : "outline-primary"}
              size="sm"
              style={{
                borderRadius: "999px",
                fontSize: "11px",
                padding: "4px 12px",
                fontWeight: 600,
                transition: "all 0.2s ease",
              }}
            >
              {cat.name}
            </ToggleButton>
          );
        })}
      </ToggleButtonGroup>

      <hr className="my-3" />

      {/* SUB CATEGORY */}
      <span
        className="text-muted d-block mb-2"
        style={{ fontSize: "10px", fontWeight: 700, letterSpacing: "0.1em" }}
      >
        SUB CATEGORY
      </span>

      <ToggleButtonGroup
        type="radio"
        name="subcategory"
        value={selectedSubCategory}
        onChange={handleSubCategorySelect}
        className="d-flex flex-wrap"
        style={{ gap: "6px" }}
      >
        {filteredSubCategories.map((sub) => {
          const isSelected =
            String(selectedSubCategory) === String(sub.subCategoryId);
          return (
            <ToggleButton
              key={sub.subCategoryId}
              id={`subcategory-${sub.subCategoryId}`}
              value={sub.subCategoryId}
              variant={isSelected ? "primary" : "outline-primary"}
              size="sm"
              style={{
                borderRadius: "999px",
                fontSize: "11px",
                padding: "4px 12px",
                fontWeight: 600,
                transition: "all 0.2s ease",
              }}
            >
              {sub.name}
            </ToggleButton>
          );
        })}
      </ToggleButtonGroup>

      <hr className="my-3" />

      {/* ACTIONS */}
      <div className="d-flex flex-column gap-2">
        <Button
          variant="primary"
          className="w-100"
          style={{ fontSize: "11px", fontWeight: 700, letterSpacing: "0.08em" }}
          onClick={handleApply}
        >
          APPLY FILTERS
        </Button>

        <Button
          variant="outline-secondary"
          className="w-100"
          style={{ fontSize: "11px", fontWeight: 600 }}
          onClick={handleReset}
        >
          RESET
        </Button>
      </div>
    </aside>
  );
};

export default Sidebar;
