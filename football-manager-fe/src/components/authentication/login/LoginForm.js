import * as Yup from 'yup';
import { useState } from 'react';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { useFormik, Form, FormikProvider } from 'formik';
import axios from 'axios';
import { Icon } from '@iconify/react';
import eyeFill from '@iconify/icons-eva/eye-fill';
import eyeOffFill from '@iconify/icons-eva/eye-off-fill';
import { inputLabelClasses } from '@mui/material/InputLabel';
// material
import {
  Link,
  Stack,
  Checkbox,
  TextField,
  IconButton,
  InputAdornment,
  FormControlLabel,
  Button
} from '@mui/material';
import account from './account';

// ----------------------------------------------------------------------

export default function LoginForm() {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);

  const LoginSchema = Yup.object().shape({
    email: Yup.string().email('Email must be a valid email address').required('Email is required'),
    password: Yup.string().required('Password is required')
  });

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
      remember: true
    },
    validationSchema: LoginSchema,
    onSubmit: () => {
      let data = '';
      axios
        .post('/api/account/authenticate', {
          email: formik.values.email,
          password: formik.values.password
        })
        .then(
          (response) => {
            data = response.data;
            try {
              console.log(response);
              if (response.statusText === 'OK') {
                account.displayName = data.userName;
                account.email = data.email;
                account.accessToken = data.token;
                account.loggedIn = true;
                sessionStorage.setItem('user', JSON.stringify(account));
                navigate('/dashboard', { replace: true });
              }
              if (data !== null && data.includes('Email')) formik.setFieldError('email', data);
              else if (data !== null && data.includes('Username'))
                formik.setFieldError('userName', data);
            } catch (ex) {
              console.log(ex);
            }
          },
          (error) => {
            data = error.response.data;
            try {
              if (data !== null && data.includes('Email')) formik.setFieldError('email', data);
              else if (data !== null && data.includes('Username'))
                formik.setFieldError('userName', data);
              console.log(error.response.statusText);
              if (error.response.statusText === 'OK') navigate('/dashboard', { replace: true });
            } catch (ex) {
              console.log(ex);
            }
          }
        );
    }
  });

  const { errors, touched, values, isSubmitting, handleSubmit, getFieldProps } = formik;

  const handleShowPassword = () => {
    setShowPassword((show) => !show);
  };

  return (
    <FormikProvider value={formik}>
      <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
        <Stack spacing={3}>
          <TextField
            fullWidth
            autoComplete="username"
            type="email"
            label="Email address"
            slabel="Standard warning"
            variant="standard"
            color="warning"
            InputLabelProps={{
              sx: {
                color: 'warning',
                [`&.${inputLabelClasses.shrink}`]: {
                  color: 'warning'
                }
              }
            }}
            {...getFieldProps('email')}
            error={Boolean(touched.email && errors.email)}
            helperText={touched.email && errors.email}
          />

          <TextField
            fullWidth
            autoComplete="current-password"
            type={showPassword ? 'text' : 'password'}
            label="Password"
            slabel="Standard warning"
            variant="standard"
            color="warning"
            InputLabelProps={{
              sx: {
                color: 'warning',
                [`&.${inputLabelClasses.shrink}`]: {
                  color: 'warning'
                }
              }
            }}
            {...getFieldProps('password')}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton onClick={handleShowPassword} edge="end">
                    <Icon icon={showPassword ? eyeFill : eyeOffFill} />
                  </IconButton>
                </InputAdornment>
              )
            }}
            error={Boolean(touched.password && errors.password)}
            helperText={touched.password && errors.password}
          />
        </Stack>

        <Stack direction="row" alignItems="center" justifyContent="space-between" sx={{ my: 2 }} />

        <Button
          fullWidth
          size="large"
          type="submit"
          variant="contained"
          loading={isSubmitting.toString()}
          style={{
            backgroundColor: '#FFD700',
            boxShadow: 'red',
            '&:hover': { background: '#efefef' }
          }}
        >
          Login
        </Button>
      </Form>
    </FormikProvider>
  );
}
