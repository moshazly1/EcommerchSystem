import { Button, Card, Col, Form, Row } from "react-bootstrap";
import useSettings from "../../Components/hooks/useSetting";
import { useState } from "react";
import { ArrowClockwise, ShieldCheck } from "react-bootstrap-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
export default function Setting() {
  const {
    data,
    setData,
    TwoFactorAuth,
    UpdateUser,
    emailDigest,
    toggleEmailDigest,
    toggelAccountActive,
    AccountActeve,
    ButtomIsEnable,
  } = useSettings();

  const handleChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  };

  console.log(TwoFactorAuth);

  return (
    <div style={{ backgroundColor: "#F9F9F9" }}>
      <h1 style={{ fontSize: "3rem" }}>Settings</h1>
      <p>
        Manage your account preferences, security protocols, and communication
        <br />
        channels from a unified command center.
      </p>
      <h2 className="my-5">Account Information</h2>
      <Card className="border-0">
        <Form className="p-4">
          <div className="row g-3">
            <Form.Group className="col-md-6">
              <Form.Label
                className="text-uppercase text-secondary fw-bold"
                style={{ fontSize: "12px" }}
              >
                Full Name
              </Form.Label>
              <Form.Control
                name="displayName"
                type="text"
                style={{ backgroundColor: "#F5F3F3" }}
                onChange={handleChange}
                value={data.displayName}
              />
            </Form.Group>

            <Form.Group className="col-md-6">
              <Form.Label
                className="text-uppercase text-secondary fw-bold"
                style={{ fontSize: "12px" }}
              >
                Email Address
              </Form.Label>
              <Form.Control
                name="email"
                type="email"
                style={{ backgroundColor: "#F5F3F3" }}
                value={data.email}
                readOnly
              />
            </Form.Group>

            <Form.Group className="col-md-6">
              <Form.Label
                className="text-uppercase text-secondary fw-bold"
                style={{ fontSize: "12px" }}
              >
                Phone Number
              </Form.Label>
              <Form.Control
                name="phoneNumber"
                type="tel"
                placeholder="+1 (555) 000-0000"
                style={{ backgroundColor: "#F5F3F3" }}
                onChange={handleChange}
                value={data.phoneNumber}
              />
            </Form.Group>

            <Form.Group className="col-md-6">
              <Form.Label
                className="text-uppercase text-secondary fw-bold"
                style={{ fontSize: "12px" }}
              >
                Timezone
              </Form.Label>
              <Form.Select
                name="timezone"
                value={data.timezone}
                onChange={handleChange}
                style={{ backgroundColor: "#F5F3F3" }}
              >
                <option value="Africa/Cairo">Cairo (EET)</option>
                <option value="America/New_York">Eastern Time (ET)</option>
                <option value="America/Los_Angeles">Pacific Time (PT)</option>
                <option value="Europe/London">London (GMT)</option>
                <option value="Asia/Dubai">Dubai (GST)</option>
              </Form.Select>
            </Form.Group>

            <div className="d-flex justify-content-end">
              <Button className="w-auto" onClick={UpdateUser}>
                Save Changes
              </Button>
            </div>
          </div>
        </Form>
      </Card>
      <h1 className="my-5">Notification Preferences</h1>
      <Card className="p-4 shadow-sm border-0 rounded-4">
        <div className="d-flex justify-content-between align-items-center my-4">
          <div>
            <h4 className="mb-1 fw-bold">Email Digest</h4>

            <p className="text-muted mb-0">
              Receive weekly summaries of new arrivals and tech insights curated
              for you.
            </p>
          </div>

          <Form.Check
            className="fs-4"
            type="switch"
            id="email-digest-switch"
            checked={emailDigest}
            onChange={(e) => toggleEmailDigest(e.target.checked)}
          />
        </div>
        <hr />
        <div className="d-flex justify-content-between align-items-center my-4">
          <div>
            <h4 className="mb-1 fw-bold">SMS Order Updates</h4>

            <p className="text-muted mb-0">
              Instant text notifications for shipping status and delivery
              confirmations.
            </p>
          </div>

          <Form.Check className="fs-4" type="switch" id="email-digest-switch" />
        </div>
        <hr />
        <div className="d-flex justify-content-between align-items-center my-4">
          <div>
            <h4 className="mb-1 fw-bold">Account Activity</h4>

            <p className="text-muted mb-0">
              Get notified of security alerts, new logins, and password changes.
            </p>
          </div>

          <Form.Check
            className="fs-4"
            type="switch"
            id="email-digest-switch"
            checked={AccountActeve}
            onChange={(e) => toggelAccountActive(e.target.checked)}
          />
        </div>
      </Card>
      <Row>
        <h2 className="my-5">Security</h2>

        <Col>
          <Card className="p-4 border-0 m-2">
            <div className="d-flex align-items-center gap-3">
              <div
                className="p-2 border rounded-3"
                style={{
                  backgroundColor: "#a7c1f9",
                  transition: "all 0.3s ease",
                }}
              >
                <ArrowClockwise size={24} color="#2563eb" />
              </div>

              <h3 className="fw-bold mb-0">Password Protocol</h3>
            </div>
            <p className="pt-3  text-secondary ">
              It is recommended to update your password every 90 days to
              maintain optimal account security.
            </p>
            <Button>Change Password</Button>
          </Card>
        </Col>

        <Col>
          <Card className="p-4 border-0 m-2">
            <div className="d-flex align-items-center gap-3">
              <div
                className="p-2 border rounded-3"
                style={{
                  backgroundColor: "#a7c1f9",
                  transition: "all 0.3s ease",
                }}
              >
                <ShieldCheck size={24} color="#2563eb" />
              </div>

              <h3 className="fw-bold mb-0">2FA Authentication</h3>
            </div>
            <p className="pt-3 text-secondary">
              Add an extra layer of protection by requiring a code from your
              mobile device upon login.
            </p>
            <Button
              onClick={() => ButtomIsEnable(!TwoFactorAuth)}
              className="border-0"
              style={{ backgroundColor: "#c6c6c6" }}
            >
              {TwoFactorAuth ? "Disable" : "Enable"} 2FA
            </Button>
          </Card>
        </Col>
      </Row>
    </div>
  );
}
