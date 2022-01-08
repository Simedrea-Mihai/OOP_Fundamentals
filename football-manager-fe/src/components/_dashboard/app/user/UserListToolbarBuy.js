import PropTypes from "prop-types";
import { Icon } from "@iconify/react";
import searchFill from "@iconify/icons-eva/search-fill";
import trash2Fill from "@iconify/icons-eva/trash-2-fill";
import roundFilterList from "@iconify/icons-ic/round-filter-list";
import { useEffect, useState, setState } from "react";
import { useNavigate } from 'react-router-dom';
import axios from "axios";
// material
import { styled } from "@mui/material/styles";
import account from "../../../../components/authentication/login/account";
import { fShortenNumber } from "../../../../utils/formatNumber";
import {
  Box,
  Toolbar,
  Tooltip,
  IconButton,
  Typography,
  OutlinedInput,
  InputAdornment,
  Card,
  Grid,
  Button,
} from "@mui/material";

// ----------------------------------------------------------------------
const RootStyle = styled(Toolbar)(({ theme }) => ({
  height: 96,
  display: "flex",
  justifyContent: "space-between",
  padding: theme.spacing(0, 1, 0, 3),
}));

const SearchStyle = styled(OutlinedInput)(({ theme }) => ({
  width: 240,
  transition: theme.transitions.create(["box-shadow", "width"], {
    easing: theme.transitions.easing.easeInOut,
    duration: theme.transitions.duration.shorter,
  }),
  "&.Mui-focused": { width: 320, boxShadow: theme.customShadows.z8 },
  "& fieldset": {
    borderWidth: `1px !important`,
    borderColor: `${theme.palette.grey[500_32]} !important`,
  },
}));

// ----------------------------------------------------------------------

UserListToolbarBuy.propTypes = {
  numSelected: PropTypes.number,
  filterName: PropTypes.string,
  onFilterName: PropTypes.func,
  budget: PropTypes.number
};

export default function UserListToolbarBuy({
  numSelected,
  filterName,
  onFilterName,
  data,
  allData,
}) {
  axios.interceptors.request.use(
    (config) => {
      config.headers.authorization = `Bearer ${account.accessToken}`;
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );
  const [team, setTeam] = useState(null);

  useEffect(() => {
    axios("/api/team/Team").then((response) => {
      setTeam(response.data);
    });
  }, []);

  const [budgetTeam, setBudgetTeam] = useState(allData.budget);

  let [marketValueList, setValue] = useState([]);
  let [ids, setIds] = useState([]);
  if (allData.id != null && data != null && allData.clicked == true && allData.buy == true) {
    if (allData.selected === false) {
      marketValueList.push(data.find((x) => x.id == allData.id).value);
      ids.push(data.find((x) => x.id == allData.id).id);
      console.log(marketValueList);
    }
  }
  if (allData.id != null && data != null && allData.clicked == false && allData.buy == true) {
    if (allData.selected == true) {
      let player = data.find((x) => x.id == allData.id);
      let index = ids.indexOf(player != null && player.id);
      console.log(allData.id, marketValueList, ids, index);
      ids.splice(index, 1);
      marketValueList.splice(index, 1);
    }
  }

  if (data != null)
    allData.sum = marketValueList.reduce(function (a, b) {
      return a + b;
    }, 0);

  return (
    <RootStyle
      sx={{
        ...(numSelected > 0 && {
          color: "primary.main",
          bgcolor: "primary.lighter",
        }),
      }}
    >
      {numSelected > 0 ? (
        <div style={{ width: "100%" }}>
          <Grid container spacing={3}>
            <Grid item xs={4}>
              <Typography component="div" variant="subtitle1">
                {numSelected} selected
              </Typography>
            </Grid>
            <Grid item xs={4} align="center">
              <Typography component="div" variant="subtitle1">
                {data !== null &&
                  budgetTeam !== 0 &&
                  `${fShortenNumber(
                    allData.sum
                  ).toUpperCase()} / ${fShortenNumber(
                    budgetTeam
                  ).toUpperCase()}`}
                {data !== null &&
                  budgetTeam == 0 &&
                  `${fShortenNumber(
                    allData.sum
                  ).toUpperCase()} / ${fShortenNumber(
                    allData.budget
                  ).toUpperCase()}`}
              </Typography>
            </Grid>
          </Grid>
        </div>
      ) : (
        <SearchStyle
          value={filterName}
          onChange={onFilterName}
          placeholder="Search user..."
          startAdornment={
            <InputAdornment position="start">
              <Box
                component={Icon}
                icon={searchFill}
                sx={{ color: "text.disabled" }}
              />
            </InputAdornment>
          }
        />
      )}
    </RootStyle>
  );
}
