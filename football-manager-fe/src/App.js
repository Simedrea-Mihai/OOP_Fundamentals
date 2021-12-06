import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react'
import { Navbar, Nav, NavItem, NavDropdown, MenuItem, NavLink } from 'react-bootstrap';
import { Container } from "reactstrap";

import { BrowserRouter, Route, Routes } from "react-router-dom";

function App() {
    return (
      <div className="App">

              <Navbar bg="light" expand="lg">
                  <Container>
                      <Navbar.Brand href="/home">World XI</Navbar.Brand>
                      <Navbar.Toggle aria-controls="basic-navbar-nav" />
                      <Navbar.Collapse id="basic-navbar-nav">
                          <Nav className="me-auto">
                          <NavLink href="/home" activeClassName="App"> Home </NavLink>
                          <NavLink href="/team" activeClassName = "Teams">Teams</NavLink>
                              <NavDropdown title="Become a legend" id="basic-nav-dropdown">
                                  <NavDropdown.Item href="/login">Login</NavDropdown.Item>
                                  <NavDropdown.Divider />
                                  <NavDropdown.Item href="/sign_in">Sign In</NavDropdown.Item>
                              </NavDropdown>
                          </Nav>
                      </Navbar.Collapse>
                  </Container>
          </Navbar>

            </div>
  );
}



export default App;
