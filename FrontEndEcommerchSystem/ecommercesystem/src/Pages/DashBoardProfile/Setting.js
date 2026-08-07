import { Button, Card, Form } from "react-bootstrap";
import useSettings from "../../Components/hooks/useSetting";

export default function Setting() {
  const { data, setData, UpdateUser } = useSettings();

  const handleChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  };

  console.log(data);
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
      <Card className=""></Card>
    </div>
  );
}
