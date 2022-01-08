import * as Yup from "yup";
import { useState, useEffect, react } from "react";
import axios from "axios";
import { Icon } from "@iconify/react";
import { useFormik, Form, FormikProvider } from "formik";
import eyeFill from "@iconify/icons-eva/eye-fill";
import eyeOffFill from "@iconify/icons-eva/eye-off-fill";
import { useNavigate } from "react-router-dom";
// material
import {
  Stack,
  TextField,
  IconButton,
  Typography,
  Button,
  Divider,
} from "@mui/material";
import DesktopDatePicker from "@mui/lab/DesktopDatePicker";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";

import { LoadingButton } from "@mui/lab";
import { inputLabelClasses } from "@mui/material/InputLabel";
import { first } from "lodash";

// ----------------------------------------------------------------------

export default function PlayerForm() {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [submitError, setSubmitError] = useState(null);
  const [exception, setException] = useState(null);

  const RegisterSchema = Yup.object().shape({
    firstName: Yup.string()
      .min(2, "Too Short!")
      .max(50, "Too Long!")
      .required("First name required"),
    lastName: Yup.string()
      .min(2, "Too Short!")
      .max(50, "Too Long!")
      .required("Last name required"),
    ovr: Yup.number().min(1).max(99),
    potential: Yup.number().min(1).max(99),
  });

  const formik = useFormik({
    initialValues: {
      firstName: "",
      lastName: "",
      ovr: 0,
      potential: 0,
    },
    validationSchema: RegisterSchema,
    onSubmit: () => {},
  });

  const [value, setValue] = useState(new Date("1992-01-01T00:00:00"));
  const [age, setAge] = useState("");
  const [pos, setPos] = useState("");

  const handleChangePos = (event) => {
    setPos(event.target.value);
  };

  const handleChangeNat = (event) => {
    setAge(event.target.value);
  };

  const handleChange = (newValue) => {
    setValue(newValue);
  };

  const { errors, touched, handleSubmit, isSubmitting, getFieldProps } = formik;

  return (
    <FormikProvider value={formik}>
      <Typography variant="h6" align="center" sx={{ pt: 5, pb: 2 }}>
        {" "}
        Create your own player{" "}
      </Typography>
      <Divider></Divider>
      <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
        <Stack spacing={3} sx={{ pt: 3 }}>
          <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
            <TextField
              fullWidth
              label="First name"
              slabel="Standard warning"
              variant="standard"
              {...getFieldProps("firstName")}
              error={Boolean(touched.firstName && errors.firstName)}
              helperText={touched.firstName && errors.firstName}
            />

            <TextField
              fullWidth
              label="Last name"
              slabel="Standard warning"
              variant="standard"
              {...getFieldProps("lastName")}
              error={Boolean(touched.lastName && errors.lastName)}
              helperText={touched.lastName && errors.lastName}
            />
          </Stack>

          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <DesktopDatePicker
              label="Birth Date"
              inputFormat="MM/dd/yyyy"
              value={value}
              onChange={handleChange}
              renderInput={(params) => (
                <TextField variant={"standard"} {...params} />
              )}
            />
          </LocalizationProvider>

          <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
            <InputLabel id="demo-simple-select-filled-label">
              Nationality
            </InputLabel>
            <Select
              labelId="demo-simple-select-filled-label"
              id="demo-simple-select-filled"
              value={age}
              onChange={handleChangeNat}
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              <MenuItem value={"ro"}>Romania</MenuItem>
              <MenuItem value={"ru"}>Russia</MenuItem>
              <MenuItem value={"it"}>Italy</MenuItem>
            </Select>
          </FormControl>
          <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
            <TextField
              fullWidth
              label="OVR"
              slabel="Standard warning"
              variant="standard"
              {...getFieldProps("ovr")}
              error={Boolean(touched.ovr && errors.ovr)}
              helperText={touched.ovr && errors.ovr}
            />

            <TextField
              fullWidth
              label="Potential"
              slabel="Standard warning"
              variant="standard"
              {...getFieldProps("potential")}
              error={Boolean(touched.potential && errors.potential)}
              helperText={touched.potential && errors.potential}
            />
          </Stack>

          <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
            <InputLabel id="demo-simple-select-filled-label">
              Position
            </InputLabel>
            <Select
              labelId="demo-simple-select-filled-label"
              id="demo-simple-select-filled"
              value={pos}
              onChange={handleChangePos}
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              <MenuItem value={"gk"}>GK</MenuItem>
              <MenuItem value={"cb"}>CB</MenuItem>
              <MenuItem value={"rb"}>RB</MenuItem>
            </Select>
          </FormControl>
          <Button
            fullWidth
            size="large"
            type="submit"
            variant="contained"
            loading={isSubmitting.toString()}
          >
            Create
          </Button>
        </Stack>
      </Form>
    </FormikProvider>
  );
}
