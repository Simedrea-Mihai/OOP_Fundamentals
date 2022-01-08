import PropTypes from "prop-types";
import { useNavigate } from "react-router-dom";
// material
import { Paper, Typography, Button } from "@mui/material";

// ----------------------------------------------------------------------

SearchNotFoundTeam.propTypes = {
  searchQuery: PropTypes.string,
};

export default function SearchNotFoundTeam({ searchQuery = "", ...other }) {
  const navigate = useNavigate();
  const playersPage = () => {
    navigate("/dashboard/user", { replace: true });
  };

  return (
    <Paper {...other}>
      <Typography gutterBottom variant="subtitle1">
        Not found
      </Typography>
      <Typography variant="body2" align="center">
        No results found for &nbsp;
        <strong>&quot;{searchQuery}&quot;</strong>. Buy a new player.
      </Typography>
      <Button onClick={playersPage}> BUY </Button>
    </Paper>
  );
}
