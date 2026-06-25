export default function Profile() {
  return (
    <div className="d-flex min-vh-100">
      <div
        className="p-5 flex-grow-1"
        style={{ backgroundColor: "var(--brand-main)" }}
      >
        <h6 className="text-uppercase text-primary fw-bold">
          Precision Selection
        </h6>

        <h1>Profile</h1>
        <p className="text-secondary pb-4">
          Curated engineering marvels for professionals who demand precision.
          Assembled with <br />
          enterprise-grade components for architectural rendering, scientific
          computing, and <br />
          creative production.
        </p>
      </div>
    </div>
  );
}
