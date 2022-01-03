import * as Yup from 'yup';
import { useState, useEffect, react } from 'react';
import axios from 'axios';
import { Icon } from '@iconify/react';
import { useFormik, Form, FormikProvider } from 'formik';
import eyeFill from '@iconify/icons-eva/eye-fill';
import eyeOffFill from '@iconify/icons-eva/eye-off-fill';
import { useNavigate } from 'react-router-dom';
// material
import { Stack, TextField, IconButton, InputAdornment, Button } from '@mui/material';
import { LoadingButton } from '@mui/lab';
import { inputLabelClasses } from '@mui/material/InputLabel';
import { first } from 'lodash';

// ----------------------------------------------------------------------

export default function RegisterForm() {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [submitError, setSubmitError] = useState(null);

  const RegisterSchema = Yup.object().shape({
    firstName: Yup.string()
      .min(2, 'Too Short!')
      .max(50, 'Too Long!')
      .required('First name required'),
    lastName: Yup.string().min(2, 'Too Short!').max(50, 'Too Long!').required('Last name required'),
    email: Yup.string().email('Email must be a valid email address').required('Email is required'),
    password: Yup.string().required('Password is required'),
    userName: Yup.string().min(2, 'Too Short!').max(50, 'Too Long!').required('Username required')
  });

  const formik = useFormik({
    initialValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      userName: ''
    },
    validationSchema: RegisterSchema,
    onSubmit: () => {
      // navigate('/dashboard', { replace: true });
      let data = '';
      axios
        .post('/api/account/register', {
          firstName: formik.values.firstName,
          lastName: formik.values.lastName,
          birthDate: '1970-12-28T00:00:00',
          email: formik.values.email,
          userName: formik.values.userName,
          password: formik.values.password
        })
        .then(
          (response) => {
            console.log(response);
            data = response.data;
            try {
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
            } catch (ex) {
              console.log(ex);
            }
          }
        );
    }
  });

  const { errors, touched, handleSubmit, isSubmitting, getFieldProps } = formik;

  return (
    <FormikProvider value={formik}>
      <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
        <Stack spacing={3}>
          <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
            <TextField
              fullWidth
              label="First name"
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
              {...getFieldProps('firstName')}
              error={Boolean(touched.firstName && errors.firstName)}
              helperText={touched.firstName && errors.firstName}
            />

            <TextField
              fullWidth
              label="Last name"
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
              {...getFieldProps('lastName')}
              error={Boolean(touched.lastName && errors.lastName)}
              helperText={touched.lastName && errors.lastName}
            />
          </Stack>

          <TextField
            fullWidth
            autoComplete="username"
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
            label="Username"
            {...getFieldProps('userName')}
            error={Boolean(touched.userName && errors.userName)}
            helperText={touched.userName && errors.userName}
          />

          <TextField
            fullWidth
            autoComplete="username"
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
            type="email"
            label="Email address"
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
                  <IconButton edge="end" onClick={() => setShowPassword((prev) => !prev)}>
                    <Icon icon={showPassword ? eyeFill : eyeOffFill} />
                  </IconButton>
                </InputAdornment>
              )
            }}
            error={Boolean(touched.password && errors.password)}
            helperText={touched.password && errors.password}
          />

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
            Register
          </Button>
        </Stack>
      </Form>
    </FormikProvider>
  );
}
