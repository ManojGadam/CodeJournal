import React from 'react';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import { useNavigate } from 'react-router-dom';  // Import useNavigate
import '../styles/Cards.css';

const placeholderImg = 'https://via.placeholder.com/150';

export default function Cards({ title, text, imageUrl, linkTo }) {
  const navigate = useNavigate();  // Initialize navigate

  return (
    <Card className="card">
      <Card.Img variant="top" src={imageUrl || placeholderImg} />
      <Card.Body>
        <Card.Title className="card-title">{title}</Card.Title>
        <Card.Text className="card-text">{text}</Card.Text>
        <Button variant="primary" onClick={() => navigate(linkTo)}>Go somewhere</Button> {/* Navigate on click */}
      </Card.Body>
    </Card>
  );
}
