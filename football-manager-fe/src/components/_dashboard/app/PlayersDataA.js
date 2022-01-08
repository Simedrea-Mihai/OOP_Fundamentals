import { Icon } from "@iconify/react";
import { filter, sample } from "lodash";
import androidFilled from "@iconify/icons-ant-design/star";
import Flag from "react-flagkit";
import { useState, useEffect, useRef } from "react";
import { sentenceCase } from "change-case";
import faker from "faker";
import axios from "axios";
// material
import { alpha, styled } from "@mui/material/styles";
import {
  Card,
  Table,
  Stack,
  Avatar,
  Button,
  Checkbox,
  TableRow,
  TableBody,
  TableCell,
  Container,
  Typography,
  TableContainer,
  TablePagination,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
  LinearProgress,
  Box
} from "@mui/material";
// utils
import { fShortenNumber } from "../../../utils/formatNumber";
// components
import Label from "./comp/Label";
import Scrollbar from "./comp/Scrollbar";
import SearchNotFound from "./comp/SearchNotFound";
import { UserListHead, UserListToolbarBuy, UserMoreMenu } from "./user";

// ----------------------------------------------------------------------

const TABLE_HEAD = [
  { id: "name", label: "Name", alignRight: false },
  { id: "position", label: "Position", alignRight: false },
  { id: "age", label: "Age", alignRight: false },
  { id: "nationality", label: "Nationality", alignRight: false },
  { id: "ovr", label: "OVR", alignRight: false },
  { id: "potential", label: "Potential", alignRight: false },
  { id: "marketValue", label: "Market Value", alignRight: false },
  { id: "buy", label: "Buy", alignRight: false },
  { id: "neg", label: "Negotiate", alignRight: false },
  { id: "" },
];

function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator(order, orderBy) {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

function applySortFilter(array, comparator, query) {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) return order;
    return a[1] - b[1];
  });
  if (query) {
    return filter(
      array,
      (_user) => _user.name.toLowerCase().indexOf(query.toLowerCase()) !== -1
    );
  }
  return stabilizedThis.map((el) => el[0]);
}

// ----------------------------------------------------------------------

const OVR = 85;
function getRandomInt(min, max) {
  min = Math.ceil(min);
  max = Math.floor(max);
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

export default function PlayersDataA({ data }) {
  const [open, setOpen] = useState(false);
  const [okOpen, setOkOpen] = useState(false);
  const [failedOpen, setFailedOpen] = useState(false);
  const [page, setPage] = useState(0);
  const [order, setOrder] = useState("asc");
  const [selected, setSelected] = useState([]);
  const [orderBy, setOrderBy] = useState("name");
  const [filterName, setFilterName] = useState("");
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [teamId, setTeamId] = useState(null);
  const [len, setLen] = useState(0);
  const [alreadyTaken, setAlreadyTaken] = useState(false);

  const [team, setTeam] = useState(null);
  const [players, setPlayers] = useState(null);

  const [allValues, setAllValues] = useState({
    id: 0,
    clicked: false,
    sum: 0,
    budget: 0,
    selected: true,
    buy: false,
  });

  const handleClickOpen = () => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget: team != null && team[0].budget,
      selected: true,
      buy: false,
    });
    setOpen(true);
  };

  const handleClose = () => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget: team != null && team[0].budget,
      selected: true,
      buy: false,
    });
    setOpen(false);
  };

  const handleCloseAlreadyTaken = () => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget: team != null && team[0].budget,
      selected: true,
      buy: false,
    });
    setAlreadyTaken(false);
  };

  const handleCloseOk = () => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget: team != null && team[0].budget,
      selected: true,
      buy: false,
    });
    setOkOpen(false);
  };

  const handleCloseFailed = () => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget: team != null && team[0].budget,
      selected: true,
      buy: false,
    });
    setFailedOpen(false);
  };

  useEffect(() => {
    axios("/api/team/Team").then((response) => {
      setTeam(response.data);
    });

    axios("/api/player/Available-Players").then((response) => {
      setPlayers(response.data);
      setLen(response.data.length);
    });
  }, []);

  useEffect(() => {
    axios("/api/player/Available-Players").then((response) => {
      setPlayers(response.data);
    });
  }, []);

  const randomTeamId = getRandomInt(1, 5);

  const handleRequestSort = (event, property) => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget:  (team != null && team[0] != null) && team[0].budget,
      selected: true,
      buy: false,
    });
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget:  (team != null && team[0] != null) && team[0].budget,
      selected: true,
      buy: false,
    });
    if (event.target.checked) {
      const newSelecteds = users.map((n) => n.name);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const users = [...Array(data != null && data.length)].map((_, index) => ({
    id: data != null && data[index].id,
    name: data != null && `${data[index].firstName} ${data[index].lastName}`,
    team: faker.company.companyName(),
    age: data != null && data[index].age,
    freeAgent: data != null && data[index].freeAgent,
    position: data != null && data[index].position,
    nationality: data != null && data[index].nationality.toUpperCase(),
    marketValue: data != null && data[index].value,
    ovr: data != null && data[index].ovr,
    potential: data != null && data[index].potential,
  }));

  const handleClick = (event, name, id, isItemSelected) => {
    const selectedIndex = selected.indexOf(name);
    //setTeamId(id);
    console.log(isItemSelected);
    if (isItemSelected === false)
      setAllValues({
        id: id,
        clicked: true,
        sum: allValues.sum,
        budget: (team != null && team[0] != null) && team[0].budget,
        selected: isItemSelected,
        buy: true,
      });
    else
      setAllValues({
        id: id,
        clicked: false,
        sum: allValues.sum,
        budget: (team != null && team[0] != null) && team[0].budget,
        selected: isItemSelected,
        buy: true,
      });
    let newSelected = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, name);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }
    setSelected(newSelected);
  };

  const handleChangePage = (event, newPage) => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget:  (team != null && team[0] != null) && team[0].budget,
      selected: true,
      buy: false,
    });
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget:  (team != null && team[0] != null) && team[0].budget,
      selected: true,
      buy: false,
    });
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleFilterByName = (event) => {
    setAllValues({
      id: 0,
      clicked: false,
      sum: allValues.sum,
      budget: (team != null && team[0] != null) && team[0].budget,
      selected: true,
      buy: false,
    });
    setFilterName(event.target.value);
  };

  const handleUserLength = (event) => {
    axios("/api/player/Available-Players").then((response) => {
      setPlayers(response.data);
      setLen(response.data.length);
    });
    return len;
  };

  const itemsRef = useRef([]);

  function createOffer(event, name, id, isItemSelected) {
    if (isItemSelected) {
      handleClickOpen();
      const decision = Math.random() < 0.5;
      window.setTimeout(() => {
        if (decision == true) {
          handleClose();
          axios.patch("/api/team/Buy-Player", {
            teamId: team[0].id,
            playerId: id,
          }).then((response) => {
            console.log(response.data);
            setOkOpen(true);
          }).catch((error) => {
            setAlreadyTaken(true);
            console.log(error);
          });
        } else if (decision == false) {
          handleClose();
          axios.patch("/api/team/Buy-Player", {
            teamId: randomTeamId,
            playerId: id,
          }).then((response) => {
            console.log(response.data);
            setFailedOpen(true);
          }).catch((error) => {
            setAlreadyTaken(true);
            console.log(error);
          });
        }
      }, 2000);
    } else {
      // add exception...
    }
  }

  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - users.length) : 0;

  const filteredUsers = applySortFilter(
    users,
    getComparator(order, orderBy),
    filterName
  );

  const isUserNotFound = filteredUsers.length === 0;

  return (
    <Card>
      <UserListToolbarBuy
        numSelected={selected.length}
        filterName={filterName}
        onFilterName={handleFilterByName}
        data={players}
        allData={allValues}
      />
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Negotating...</DialogTitle>
        <LinearProgress></LinearProgress>
        <DialogContent>
          <DialogContentText>
            Player's agent is making a decision... Please wait...
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
        </DialogActions>
      </Dialog>

      <Dialog open={okOpen} onClose={handleCloseOk}>
        <DialogTitle>Bought</DialogTitle>
        <DialogContent>
          <DialogContentText>The player was bought</DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseOk}>Ok</Button>
        </DialogActions>
      </Dialog>

      
      <Dialog open={alreadyTaken} onClose={handleCloseAlreadyTaken}>
        <DialogTitle>Taken</DialogTitle>
        <DialogContent>
          <DialogContentText>The player is already taken</DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseAlreadyTaken}>Ok</Button>
        </DialogActions>
      </Dialog>

      <Dialog open={failedOpen} onClose={handleCloseFailed}>
        <DialogTitle>Failed</DialogTitle>
        <DialogContent>
          <DialogContentText>The player transaction failed</DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseFailed}>Ok</Button>
        </DialogActions>
      </Dialog>

      <Scrollbar>
        <TableContainer sx={{ minWidth: 800 }}>
          <Table>
            <UserListHead
              order={order}
              orderBy={orderBy}
              headLabel={TABLE_HEAD}
              rowCount={users.length}
              numSelected={selected.length}
              onRequestSort={handleRequestSort}
              onSelectAllClick={handleSelectAllClick}
            />
            <TableBody>
              {filteredUsers
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row) => {
                  const {
                    id,
                    name,
                    position,
                    avatarUrl,
                    age,
                    nationality,
                    marketValue,
                    ovr,
                    potential,
                    freeAgent,
                  } = row;
                  const isItemSelected = selected.indexOf(name) !== -1;

                  return (
                    <TableRow
                      hover
                      key={id}
                      tabIndex={-1}
                      role="checkbox"
                      selected={isItemSelected}
                      aria-checked={isItemSelected}
                    >
                      <TableCell padding="normal">
                        <br />
                      </TableCell>
                      <TableCell component="th" scope="row" padding="none">
                        <Stack direction="row" alignItems="center" spacing={2}>
                          <Avatar src={avatarUrl} />
                          <Typography variant="subtitle2" noWrap>
                            {name}
                          </Typography>
                        </Stack>
                      </TableCell>
                      <TableCell align="left">{position}</TableCell>
                      <TableCell align="left">{age}</TableCell>
                      <TableCell align="left">
                        {nationality === "EN" && <Flag country="US" />}
                        {nationality !== "EN" && (
                          <Flag country={`${nationality}`} />
                        )}
                      </TableCell>
                      <TableCell align="left"> {ovr} </TableCell>
                      <TableCell align="left"> {potential} </TableCell>
                      <TableCell align="left">
                        {" "}
                        {`€ ${fShortenNumber(marketValue).toUpperCase()}`}{" "}
                      </TableCell>
                      <TableCell padding="checkbox">
                        <Checkbox
                          checked={isItemSelected}
                          onChange={(event) =>
                            handleClick(event, name, id, isItemSelected)
                          }
                          disabled={false}
                        />
                      </TableCell>
                      <TableCell>
                        {team != null && team[0] != null && (
                          <Button
                            key={id}
                            ref={(el) => (itemsRef.current[id] = el)}
                            onClick={(event) =>
                              createOffer(event, name, id, isItemSelected)
                            }
                          >
                            {" "}
                            Make an offer{" "}
                          </Button>
                        )}
                        {team == null || team[0] == null && (
                          <div> No team </div>
                        )}
                      </TableCell>
                    </TableRow>
                  );
                })}
              {emptyRows > 0 && (
                <TableRow style={{ height: 53 * emptyRows }}>
                  <TableCell colSpan={8} />
                </TableRow>
              )}
            </TableBody>
            {isUserNotFound && (
              <TableBody>
                <TableRow>
                  <TableCell align="center" colSpan={12} sx={{ py: 3 }}>
                    <SearchNotFound searchQuery={filterName} />
                  </TableCell>
                </TableRow>
              </TableBody>
            )}
          </Table>
        </TableContainer>
      </Scrollbar>

      <TablePagination
        rowsPerPageOptions={[5, 10, 25]}
        component="div"
        count={users.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </Card>
  );
}
