// material
import Flippy, { FrontSide, BackSide } from "react-flippy";
import { Box, Grid, Container, Typography, Card } from "@mui/material";
import ReactTooltip from "react-tooltip";
import { useState } from "react";
import { Icon } from "@iconify/react";
import Stack from "@mui/material/Stack";
import { alpha, styled } from "@mui/material/styles";

export default function SlideCards({ teamOVR, teamPotential, teamData }) {
  const RootStyle = styled(Card)(({ theme }) => ({
    boxShadow: "none",
    textAlign: "center",
    padding: theme.spacing(2, 0),
    color: theme.palette.warning.darker,
    backgroundColor: "transparent",
  }));

  const IconWrapperStyle = styled("div")(({ theme }) => ({
    margin: "auto",
    display: "flex",
    borderRadius: "50%",
    alignItems: "center",
    width: theme.spacing(8),
    height: theme.spacing(8),
    justifyContent: "center",
    marginBottom: theme.spacing(3),
    color: theme.palette.warning.dark,
    backgroundImage: `linear-gradient(135deg, ${alpha(
      theme.palette.warning.dark,
      0
    )} 0%, ${alpha(theme.palette.warning.dark, 0.24)} 100%)`,
  }));

  const RootStyle2 = styled(Card)(({ theme }) => ({
    boxShadow: "none",
    textAlign: "center",
    padding: theme.spacing(5, 0),
    color: theme.palette.primary.darker,
    backgroundColor: "transparent",
  }));

  const RootStyle3 = styled(Card)(({ theme }) => ({
    boxShadow: "none",
    textAlign: "center",
    padding: theme.spacing(5, 0),
    color: theme.palette.primary.darker,
    backgroundColor: "transparent",
  }));
  const IconWrapperStyle2 = styled("div")(({ theme }) => ({
    margin: "auto",
    display: "flex",
    borderRadius: "50%",
    alignItems: "center",
    width: theme.spacing(8),
    height: theme.spacing(8),
    justifyContent: "center",
    marginBottom: theme.spacing(3),
    color: theme.palette.primary.dark,
    backgroundImage: `linear-gradient(135deg, ${alpha(
      theme.palette.primary.dark,
      0
    )} 0%, ${alpha(theme.palette.primary.dark, 0.24)} 100%)`,
  }));

  return (
    <Stack spacing={2}>
      <Grid item>
        <Flippy flipDirection="vertical" flipOnHover={1}>
          <FrontSide
            style={{
              backgroundColor: "#C8FACD",
              borderRadius: 15,
              textAlign: "center",
              height: 234,
            }}
          >
            <RootStyle2>
              <IconWrapperStyle2>
                <Icon
                  icon="fluent:text-description-20-filled"
                  width={24}
                  height={24}
                />
              </IconWrapperStyle2>
              <Typography variant="h4">
                {" "}
                {teamData != null && teamData.name}{" "}
              </Typography>
              <Typography variant="subtitle2" sx={{ opacity: 0.72 }}>
                Description
              </Typography>
            </RootStyle2>
          </FrontSide>
          <BackSide
            style={{
              backgroundColor: "#C8FACD",
              borderRadius: 15,
            }}
          >
            <RootStyle3>
              <IconWrapperStyle2>
                <Icon icon="bi:chat-left-text" width={24} height={24} />
              </IconWrapperStyle2>
              <Typography variant="subtitle2">
                {teamData != null && teamData.headerDescription}
              </Typography>
              <Typography variant="subtitle2" sx={{ opacity: 0.72 }}>
                {teamData != null && teamData.description}
              </Typography>
            </RootStyle3>
          </BackSide>
        </Flippy>
      </Grid>
    </Stack>
  );
}
