import { Container, Row, Col } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/Footer.css'; // Import the CSS file

export default function Footer() {
  return (
    <footer className="footer">
      <Container>
        <Row>
          <Col className="text-center">
            <p>&copy; {new Date().getFullYear()} Code Journal. All rights reserved.</p>
            <p>
              Follow us on:
              <a href="https://github.com/ManojGadam" className="footer-link"> Github</a>,
              <a href="https://www.linkedin.com/in/manojgadamchetty/" className="footer-link"> Linkedin</a>
            </p>
          </Col>
        </Row>
      </Container>
    </footer>
  );
}
