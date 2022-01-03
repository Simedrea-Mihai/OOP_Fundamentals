// material
import { Box, Grid, Container, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import axios from 'axios';
// components
import Page from '../components/Page';
import {
  BestMidfielder,
  BestGoalkeeper,
  BestDefender,
  BestStriker,
  TeamChart,
  PlayersInfo,
  Map,
  SlideCards,
  SkeletonPH
} from '../components/_dashboard/app';
import account from '../components/authentication/login/account';
import NavColors from '../components/NavColor';

function getRandomInt(min, max) {
  min = Math.ceil(min);
  max = Math.floor(max);
  return Math.floor(Math.random() * (max - min + 1)) + min;
}
// ----------------------------------------------------------------------

export default function DashboardApp() {
  const id = getRandomInt(1, 5);

  const [dataST, setDataST] = useState(null);
  const [dataCAM, setDataCAM] = useState(null);
  const [dataCB, setDataCB] = useState(null);
  const [dataGK, setDataGK] = useState(null);

  const urlST = `/api/player/Player-By-Position?Position=ST&TeamId=${id}`;
  const urlCAM = `/api/player/Player-By-Position?Position=CAM&TeamId=${id}`;
  const urlCB = `/api/player/Player-By-Position?Position=CB&TeamId=${id}`;
  const urlGK = `/api/player/Player-By-Position?Position=GK&TeamId=${id}`;

  const url = `/api/team/Team/${id}`;
  const [data, setData] = useState(null);

  const urlCountry = `/api/team/Team-Nationality/${id}`;
  const [countryData, setCountryData] = useState(null);

  useEffect(() => {
    axios(url).then((response) => {
      setData(response.data);
    });
  }, []);

  useEffect(() => {
    axios(urlST).then((response) => {
      setDataST(response.data);
    });
    axios(urlCAM).then((response) => {
      setDataCAM(response.data);
    });
    axios(urlCB).then((response) => {
      setDataCB(response.data);
    });
    axios(urlGK).then((response) => {
      setDataGK(response.data);
    });
  }, []);

  useEffect(() => {
    axios(urlCountry).then((response) => {
      setCountryData(response.data);
    });
  }, []);

  let teamOVR = 0;
  let teamPotential = 0;
  if (data != null) {
    for (let i = 0; i < data.players.length; i += 1) {
      teamOVR += data.players[i].ovr;
      teamPotential += data.players[i].potential;
    }
    teamOVR /= data.players.length;
    teamOVR = Math.round(teamOVR);

    teamPotential /= data.players.length;
    teamPotential = Math.round(teamPotential);
  }

  // set requests for countries
  return (
    <Page title="Dashboard | FootballManager">
      <Container maxWidth="xl">
        <Box sx={{ pb: 5 }}>
          <Typography variant="h4">Become a legend</Typography>
        </Box>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={6} md={3}>
            {dataST !== null && dataST[0] !== Array.Empty && <BestStriker props={dataST} />}
            {(dataST === null || dataST[0] === Array.Empty) && <SkeletonPH />}
          </Grid>
          <Grid item xs={12} sm={6} md={3}>
            {dataCAM !== null && dataCAM[0] !== Array.Empty && <BestMidfielder props={dataCAM} />}
            {(dataCAM === null || dataCAM[0] === Array.Empty) && <SkeletonPH />}
          </Grid>
          <Grid item xs={12} sm={6} md={3}>
            {dataCB !== null && dataCB[0] !== Array.Empty && <BestDefender props={dataCB} />}
            {(dataCB === null || dataCB[0] === Array.Empty) && <SkeletonPH />}
          </Grid>
          <Grid item xs={12} sm={6} md={3}>
            {dataGK !== null && dataGK[0] !== Array.Empty && <BestGoalkeeper props={dataGK} />}
            {(dataGK === null || dataGK[0] === Array.Empty) && <SkeletonPH />}
          </Grid>

          <Grid item lg={9}>
            <TeamChart data={data} />
          </Grid>

          <Grid item lg={3}>
            <SlideCards teamOVR={teamOVR} teamPotential={teamPotential} teamData={data} />
          </Grid>

          <Grid item xs={16} sm={6} md={12}>
            <PlayersInfo data={data} />
          </Grid>

          <Grid item xs={16} sm={6} md={12}>
            <Map data={countryData} />
          </Grid>
        </Grid>
      </Container>
    </Page>
  );
}
