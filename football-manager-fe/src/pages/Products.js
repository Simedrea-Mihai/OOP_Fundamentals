import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
// material
import { styled } from '@mui/material/styles';
import { Stack, Grid, Container, Typography, Box, Button, Divider, Toolbar } from '@mui/material';
// layouts
import AuthLayout from '../layouts/AuthLayout';
// components
import Page from '../components/Page';
import { MHidden } from '../components/@material-extend';
import { LoginForm } from '../components/authentication/login';
import AuthSocial from '../components/authentication/AuthSocial';
import NavColors from '../components/NavColor';

// ----------------------------------------------------------------------

const RootStyle = styled(Page)(({ theme }) => ({
  [theme.breakpoints.up('md')]: {
    display: 'flex'
  }
}));

const SectionStyle = styled(Box)(({ theme }) => ({
  width: '100%',
  maxWidth: 450,
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center',
  backgroundColor: 'white',
  margin: 'auto'
}));

const ContentStyle = styled('div')(({ theme }) => ({
  maxWidth: 420,
  margin: 'auto',
  display: 'flex',
  minHeight: '20vh',
  flexDirection: 'column',
  justifyContent: 'center',
  padding: theme.spacing(12, 0)
}));

// ----------------------------------------------------------------------

export default function Team() {
  const navigate = useNavigate();

  const myTeamPage = () => {
    navigate('/dashboard/myTeam', { replace: true });
  };

  return (
    <RootStyle title="Team | FootballManager">
      <Container maxWidth="sm">
        <ContentStyle>
          <Stack sx={{ mb: 5 }}>
            <Typography variant="h4" gutterBottom>
              My team
            </Typography>
            <Typography sx={{ color: 'text.secondary' }}>
              Don't you have a team? - Create you team now
            </Typography>
            <Button
              fullWidth
              size="large"
              type="submit"
              variant="contained"
              style={{
                backgroundColor: '#27599c',
                boxShadow: 'none',
                '&:hover': { background: '#efefef' },
                shadowColor: 'white'
              }}
              onClick={myTeamPage}
              sx={{ mt: 3 }}
            >
              Create
            </Button>
            <Divider sx={{ my: 1 }} />
            <Button
              fullWidth
              size="large"
              type="submit"
              variant="contained"
              style={{
                backgroundColor: '#27599c',
                boxShadow: 'none',
                '&:hover': { background: '#efefef' },
                shadowColor: 'white'
              }}
              sx={{ mt: 0 }}
            >
              Search for others
            </Button>
          </Stack>
        </ContentStyle>
      </Container>
      <MHidden width="mdDown">
        <SectionStyle>
          <img src="/static/illustrations/wallpaper_team.jpg" alt="login" />
        </SectionStyle>
      </MHidden>
    </RootStyle>
  );
}
