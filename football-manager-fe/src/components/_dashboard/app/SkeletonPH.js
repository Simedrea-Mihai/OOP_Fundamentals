// material
import { alpha, styled } from '@mui/material/styles';
import { Card, Typography, Paper } from '@mui/material';
import Skeleton from '@mui/material/Skeleton';
// ----------------------------------------------------------------------

const RootStyle = styled(Card)(({ theme }) => ({
  boxShadow: 'none',
  textAlign: 'center',
  padding: theme.spacing(5, 0),
  color: theme.palette.primary.darker,
  backgroundColor: 'transparent'
}));

const IconWrapperStyle = styled('div')(({ theme }) => ({
  margin: 'auto',
  display: 'flex',
  borderRadius: '50%',
  alignItems: 'center',
  width: theme.spacing(8),
  height: theme.spacing(8),
  justifyContent: 'center',
  marginBottom: theme.spacing(3),
  color: theme.palette.primary.dark
}));

// ----------------------------------------------------------------------

export default function SkeletonPH() {
  return (
    <Paper>
      <Skeleton variant="text" />
      <Skeleton variant="circular" width={40} height={40} />
      <Skeleton variant="rectangular" height={195} style={{ borderRadius: 15 }} />
    </Paper>
  );
}
