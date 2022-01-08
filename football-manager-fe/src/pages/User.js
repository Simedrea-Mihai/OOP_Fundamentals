import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { filter, sample } from "lodash";
import faker from "faker";
// material
import { Box, Grid, Container, Typography, Card } from "@mui/material";
import Page from "../components/Page";
import axios from "axios";
// utils
// components
import PlayersDataA from "../components/_dashboard/app/PlayersDataA";
import PlayersDataU from "../components/_dashboard/app/PlayersDataU";
import PlayerForm from "../components/_dashboard/app/PlayerForm";
// ----------------------------------------------------------------------

export default function User() {

  const [dataA, setDataA] = useState(null);
  const urlA = "/api/player/Available-Players";

  const [dataU, setDataU] = useState(null);
  const urlU = "/api/player/Unavailable-Players";

  useEffect(() => {
    axios(urlA).then((response) => {
      setDataA(response.data);
    });
    axios(urlU).then((response) => {
      setDataU(response.data);
    });
  }, []);

  return (
    <Page title="Players | FootballManager">
      <Container maxWidth="xl">
        <Box sx={{ pb: 1 }}>
          <Typography variant="h4">Players</Typography>
        </Box>
        <Box sx={{ pb: 4 }}>
          <Typography variant="h8">Look for other players</Typography>
        </Box>
        <Grid container spacing={3}>
          <Grid item xs={6} sm={6} md={6}>
            <PlayerForm/>
          </Grid>
          <Grid item xs={6} sm={6} md={6}>
          <Card align="center">
              <img
                src="/static/illustrations/wallpaper_player2.jpg"
              />
            </Card>
          </Grid>
          <Grid item xs={12} sm={12} md={12}>
            <PlayersDataA data={dataA} />
          </Grid>
          <Grid item xs={12} sm={12} md={12}>
            <PlayersDataU data={dataU} />
          </Grid>
        </Grid>
      </Container>
    </Page>
  );
}
