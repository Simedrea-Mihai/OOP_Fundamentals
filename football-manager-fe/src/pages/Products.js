import { Link as RouterLink, useNavigate } from "react-router-dom";
import { useState, useEffect, useRef } from "react";
// material
import { styled } from "@mui/material/styles";
import axios from "axios";
import {
  Stack,
  Grid,
  Container,
  Typography,
  Box,
  Button,
  Divider,
  Toolbar,
} from "@mui/material";
// layouts
import AuthLayout from "../layouts/AuthLayout";
// components
import Page from "../components/Page";
import { MHidden } from "../components/@material-extend";
import { LoginForm } from "../components/authentication/login";
import AuthSocial from "../components/authentication/AuthSocial";
import NavColors from "../components/NavColor";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import account from "../components/authentication/login/account";

// ----------------------------------------------------------------------

axios.interceptors.request.use(
  (config) => {
    config.headers.authorization = `Bearer ${account.accessToken}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

const RootStyle = styled(Page)(({ theme }) => ({
  [theme.breakpoints.up("md")]: {
    display: "flex",
  },
}));

const SectionStyle = styled(Box)(({ theme }) => ({
  width: "100%",
  maxWidth: 450,
  display: "flex",
  flexDirection: "column",
  justifyContent: "center",
  backgroundColor: "white",
  margin: "auto",
}));

const ContentStyle = styled("div")(({ theme }) => ({
  maxWidth: 420,
  margin: "auto",
  display: "flex",
  minHeight: "20vh",
  flexDirection: "column",
  justifyContent: "center",
  padding: theme.spacing(12, 0),
}));

// ----------------------------------------------------------------------

export default function Team() {
  const [open, setOpen] = useState(false);
  const nameRef = useRef('');
  const headerDescRef = useRef('');
  const descRef = useRef('');

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const navigate = useNavigate();

  const [teamLen, setTeamLen] = useState(null);

  useEffect(() => {
    axios("/api/team/Team").then((response) => {
      setTeamLen(response.data);
    });
  }, []);

  if (teamLen !== null && teamLen.length > 0)
    navigate("/dashboard/myTeam", { replace: true });

  const myTeamPage = () => {
    axios.post("/api/team/Team", {
      name: nameRef.current.value,
      description: descRef.current.value,
      headerDescription: headerDescRef.current.value,
      budget: 0,
      userId: account.userId,
    });
    navigate("/dashboard/myTeam", { replace: true });
  };

  return (
    <RootStyle title="Team | FootballManager">
      <Container maxWidth="sm">
        <ContentStyle>
          <Stack sx={{ mb: 5 }}>
            <Typography variant="h4" gutterBottom>
              My team
            </Typography>
            <Typography sx={{ color: "text.secondary" }}>
              Don't you have a team? - Create your team now
            </Typography>
            <Button
              fullWidth
              size="large"
              type="submit"
              variant="contained"
              style={{
                backgroundColor: "#27599c",
                boxShadow: "none",
                "&:hover": { background: "#efefef" },
                shadowColor: "white",
              }}
              onClick={handleClickOpen}
              sx={{ mt: 3 }}
            >
              Create
            </Button>
            <Dialog open={open} onClose={handleClose}>
              <DialogTitle>My Team</DialogTitle>
              <DialogContent>
                <DialogContentText>
                  Begin something great right now! Be the next big thing in
                  footbal.
                </DialogContentText>
                <TextField
                  autoFocus
                  margin="dense"
                  id="name"
                  label="Name"
                  type="username"
                  fullWidth
                  variant="standard"
                  inputRef={nameRef}
                />
                <TextField
                  autoFocus
                  margin="dense"
                  id="headerDescription"
                  label="Header Description"
                  type="username"
                  fullWidth
                  variant="standard"
                  inputRef={headerDescRef}
                />
                <TextField
                  autoFocus
                  margin="dense"
                  id="description"
                  label="Description"
                  type="username"
                  fullWidth
                  variant="standard"
                  inputRef={descRef}
                />
              </DialogContent>
              <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <Button onClick={myTeamPage}>Create</Button>
              </DialogActions>
            </Dialog>
            <Divider sx={{ my: 1 }} />
            <Button
              fullWidth
              size="large"
              type="submit"
              variant="contained"
              style={{
                backgroundColor: "#27599c",
                boxShadow: "none",
                "&:hover": { background: "#efefef" },
                shadowColor: "white",
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
