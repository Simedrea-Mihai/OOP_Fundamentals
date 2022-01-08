import { Icon } from "@iconify/react";
import { filter, sample } from "lodash";
import androidFilled from "@iconify/icons-ant-design/star";
import Flag from "react-flagkit";
import { useState } from "react";
import { sentenceCase } from "change-case";
import faker from "faker";
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
} from "@mui/material";
// utils
import { fShortenNumber } from "../../../utils/formatNumber";
// components
import Label from "./comp/Label";
import Scrollbar from "./comp/Scrollbar";
import SearchNotFoundTeam from "./comp/SearchNotFoundTeam";
import { UserListHead, UserListToolbar, UserMoreMenu } from "./user";

// ----------------------------------------------------------------------

const TABLE_HEAD = [
  { id: "name", label: "Name", alignRight: false },
  { id: "position", label: "Position", alignRight: false },
  { id: "age", label: "Age", alignRight: false },
  { id: "nationality", label: "Nationality", alignRight: false },
  { id: "ovr", label: "OVR", alignRight: false },
  { id: "potential", label: "Potential", alignRight: false },
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

export default function PlayersDataTeam({ data }) {
  const [page, setPage] = useState(0);
  const [order, setOrder] = useState("asc");
  const [selected, setSelected] = useState([]);
  const [orderBy, setOrderBy] = useState("name");
  const [filterName, setFilterName] = useState("");
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = users.map((n) => n.name);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const users = [...Array(data != null && data.length)].map((_, index) => ({
    id: faker.datatype.uuid(),
    name: data != null && `${data[index].firstName} ${data[index].lastName}`,
    team: faker.company.companyName(),
    age: data != null && data[index].age,
    position: data != null && data[index].position,
    nationality: data != null && data[index].nationality.toUpperCase(),
    ovr: data != null && data[index].ovr,
    potential: data != null && data[index].potential,
  }));

  const handleClick = (event, name) => {
    const selectedIndex = selected.indexOf(name);
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
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleFilterByName = (event) => {
    setFilterName(event.target.value);
  };

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
      <UserListToolbar
        numSelected={selected.length}
        filterName={filterName}
        onFilterName={handleFilterByName}
      />
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
                    ovr,
                    potential,
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
                      <TableCell>
                        <UserMoreMenu />
                      </TableCell>
                    </TableRow>
                  );
                })}
              {emptyRows > 0 && (
                <TableRow style={{ height: 53 * emptyRows }}>
                  <TableCell colSpan={6} />
                </TableRow>
              )}
            </TableBody>
            {isUserNotFound && (
              <TableBody>
                <TableRow>
                  <TableCell align="center" colSpan={8} sx={{ py: 3 }}>
                    <SearchNotFoundTeam searchQuery={filterName} />
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
