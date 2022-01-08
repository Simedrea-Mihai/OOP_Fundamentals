import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
// material
import { styled } from "@mui/material/styles";
import { Card, Grid, Box, Typography, Container, Button } from "@mui/material";
// components
import Page from "../components/Page";
import { fShortenNumber } from "../utils/formatNumber";
import PlayersDataU from "../components/_dashboard/app/PlayersDataU";
import account from '../components/authentication/login/account';
import {
  PlayersDataTeam,
  SlideCardsMyTeam,
  SlideCardsMyTeam2,
  TeamChartMyTeam,
} from "../components/_dashboard/app";

axios.interceptors.request.use(
  (config) => {
    config.headers.authorization = `Bearer ${account.accessToken}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default function MyTeam() {

  const [team, setTeam] = useState(null);
  const [id, setId] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    axios("/api/team/Team").then((response) => {
      setTeam(response.data);
      setId(response.data[0].id);
    });
  }, []);

  const teamDeleteFunction = () => {
    axios.delete(`/api/team/Team/${team[0].id}`).then((response) => {
      console.log(response);
      navigate("/dashboard/teams", { replace: true });
    });
  };

  let teamOVR = 0;
  let teamPotential = 0;
  if (team != null && team[0] != null) {
    for (let i = 0; i < team[0].players.length; i += 1) {
      teamOVR += team[0].players[i].ovr;
      teamPotential += team[0].players[i].potential;
    }
    teamOVR /= team[0].players.length;
    teamOVR = Math.round(teamOVR);

    teamPotential /= team[0].players.length;
    teamPotential = Math.round(teamPotential);
  }

  return (
    <Page title="My Team | FootballManager">
      <Container maxWidth="xl">
        <Box sx={{ pb: 1 }}>
          {team !== null && (
            <Typography variant="h4">
              {team[0] != null && team[0].name}
            </Typography>
          )}
          {team === null && <Typography variant="h4">My Team</Typography>}
        </Box>
        <Box sx={{ pb: 5 }}>
          {team !== null && (
            <Typography variant="h8">
              {team[0] != null && team[0].headerDescription}
            </Typography>
          )}
          {team === null && <Typography variant="h8">Loading...</Typography>}
        </Box>
        <Grid container spacing={3}>
          <Grid item xs={3}>
            <Typography align="center" variant="h6">
              Bugdet
            </Typography>
            {team !== null && (
              <Card align="center" sx={{ pt: 1, m: 1, pb: 1 }}>
                {`€ ${fShortenNumber(
                  team[0] != null && team[0].budget
                ).toUpperCase()}`}{" "}
              </Card>
            )}
            {team === null && (
              <Card align="center" sx={{ pt: 1, m: 1, pb: 1 }}>
                Loading...
              </Card>
            )}
            <Grid item xs={12} sx={{ pt: 3 }}>
              <SlideCardsMyTeam
                teamOVR={teamOVR}
                teamPotential={teamPotential}
                teamData={team}
              />
            </Grid>
          </Grid>
          <Grid item xs={6}>
            <Card align="center">
              <img
                src="/static/illustrations/football_pitch.jpg"
              />
            </Card>
          </Grid>
          <Grid item xs={3}>
            <Typography align="center" variant="h6">
              Manager
            </Typography>
            {team !== null && (
              <Card align="center" sx={{ pt: 1, m: 1, pb: 1 }}>
                {"some name"}
              </Card>
            )}
            {team === null && (
              <Card align="center" sx={{ pt: 1, m: 1, pb: 1 }}>
                Loading...
              </Card>
            )}
            <Grid item xs={12} sx={{ pt: 3 }}>
              <SlideCardsMyTeam2
                teamOVR={teamOVR}
                teamPotential={teamPotential}
                teamData={team != null && team[0]}
              />
            </Grid>
          </Grid>
          {team !== null && team[0].players.length > 0 && (
            <Grid item xs={12}>
              {team !== null && (
                <TeamChartMyTeam data={team[0] != null && team[0]} />
              )}
              {team !== null && team[0] == null && <Card> Loading... </Card>}
            </Grid>
          )}

          <Grid item xs={12}>
            {team !== null && (
              <PlayersDataTeam
                data={team[0] != null && team[0].players}
              ></PlayersDataTeam>
            )}
            {team !== null && team[0] == null && <Card> Loading... </Card>}
          </Grid>
          <Grid item xs={12} align="center">
            <Button onClick={teamDeleteFunction}> Delete </Button>
          </Grid>
        </Grid>
      </Container>
    </Page>
  );
}
