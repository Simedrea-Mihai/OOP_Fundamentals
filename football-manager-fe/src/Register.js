import React, { Component, useEffect, useState } from "react";
import axios from 'axios';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

function Register() {

    return (

        <div className="App">
            <br />
            <div className="FormDiv">
                <Form>
                    <Form.Group className="mb-3" controlId="formFirstName">
                        <Form.Label>FirstName</Form.Label>
                        <Form.Control type="FirstName" placeholder="" />
                        <Form.Text className="text-muted">
                        </Form.Text>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formLastName">
                        <Form.Label>LastName</Form.Label>
                        <Form.Control type="LastName" placeholder="" />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBirthDate">
                        <Form.Label>BirthDate</Form.Label>
                        <Form.Control type="BirthDate" placeholder="" />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formEmail">
                        <Form.Label>Email</Form.Label>
                        <Form.Control type="Email" placeholder="" />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formUserName">
                        <Form.Label>Username</Form.Label>
                        <Form.Control type="Username" placeholder="" />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formPassword">
                        <Form.Label>Password</Form.Label>
                        <Form.Control type="Password" placeholder="" />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formButton">
                        <Button variant="outline-primary"> Add data </Button>
                    </Form.Group>

                </Form>
            </div>
            <br />
        </div>
        
        
    );


}

export default Register