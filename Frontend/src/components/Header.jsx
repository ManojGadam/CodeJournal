import { Navbar, Nav } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/Header.css';

export default function Header() {
  return (
    <Navbar className="navbar-custom" expand="lg">
      <Navbar.Brand href="#home">CodeJournal</Navbar.Brand>
      <Nav className="ml-auto">
        <Nav.Link href="#home">
          <i className="fas fa-user icon"></i> {/* Profile icon */}
        </Nav.Link>
      </Nav>
    </Navbar>
  );
}
