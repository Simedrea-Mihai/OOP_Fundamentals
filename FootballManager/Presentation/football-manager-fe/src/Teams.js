import React, { Component, useEffect, useState } from "react";
import axios from 'axios';
import Button from 'react-bootstrap/Button';

import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';

import { Line } from 'react-chartjs-2';


ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
);

function Teams() {


    const url = '/api/team/Team';
    const [team, setTeam] = useState(null);

    const [addedTeam, updateTeam] = useState({
        name: "testteam",
        budget: 1234
    });


    useEffect(() => {
        axios.get(url)
            .then(response => {
                setTeam(response.data)
            })
    }, [url])

    const postdata = () => {

        axios.post(url, {
            "name": "testteam",
            "budget": 123456789
        })
            .then((response) => {
                console.log(response);
                window.location.reload();
            }, (error) => {
                console.log(error);
            });


    }

    if (team) {

        const data = {
            labels: team.map(t => t.name),
            datasets: [
                {
                    label: "budget",
                    data: team.map(t => t.budget),
                    fill: true,
                    backgroundColor: "#0069d9",
                    pointBorderColor: "white",
                    pointBorderWidth: 2,
                    pointRadius: 8,
                    tension: 0.4
                },
            ],
        };

        const options = {
            plugins: { legend: { display: true } },
            layout: { padding: { bottom: 100 } },

        };

        return (
            
            <div className="App">
                <Line data={data} options={options} /> 
                <Button variant="outline-primary" onClick={() => postdata()}> Add data </Button>
                <h1> <br/> Teams data </h1>
                <h1> {team.name} </h1>
                <table className='table table-stripped'>
                    <thead>
                        <tr>

                            <th>
                                TeamId
                            </th>
                            <th>
                                Name
                            </th>

                            <th>
                                Budget
                            </th>

                        </tr>

                    </thead>
                    <tbody>
                        {team.map(
                            t =>
                                <tr key={t.id}>
                                    <td> {t.id} </td>
                                    <td> {t.name} </td>
                                    <td> {t.budget} </td>
                                </tr>

                        )}

                    </tbody>

                </table>

            </div>

        );
    }
    else {
        return (
            
            <div>

            </div>    
            
        )

    }
}

export default Teams
