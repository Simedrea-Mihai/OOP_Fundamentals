import PropTypes from 'prop-types';
// material
import { alpha, styled } from '@mui/material/styles';

// ----------------------------------------------------------------------

const RootStyle = styled('span')(({ theme, ownerState }) => {
  const { color, variant } = ownerState;
  const styleAvailable = (color) => ({
    color: theme.palette.dark,
    backgroundColor: 'green'
  });

  const styleUnavailable = (color) => ({
    color: theme.palette.dark,
    backgroundColor: 'red'
  });

  return {
    height: 22,
    minWidth: 22,
    lineHeight: 0,
    borderRadius: 8,
    cursor: 'default',
    alignItems: 'center',
    whiteSpace: 'nowrap',
    display: 'inline-flex',
    justifyContent: 'center',
    padding: theme.spacing(0, 1),
    color: theme.palette.grey[800],
    fontSize: theme.typography.pxToRem(12),
    fontFamily: theme.typography.fontFamily,
    backgroundColor: theme.palette.grey[300],
    fontWeight: theme.typography.fontWeightBold,

    ...(color !== 'default'
      ? {
          ...(variant === 'available' && { ...styleAvailable(color) }),
          ...(variant === 'unavailable' && { ...styleUnavailable(color) })
        }
      : {
          ...(variant === 'available' && {
            color: 'white',
            backgroundColor: '#4caf50'
          }),
          ...(variant === 'unavailable' && {
            color: 'white',
            backgroundColor: '#b00020'
          })
        })
  };
});

// ----------------------------------------------------------------------

export default function Label({ color = 'default', variant = 'available', children, ...other }) {
  return (
    <RootStyle ownerState={{ color, variant }} {...other}>
      {children}
    </RootStyle>
  );
}

Label.propTypes = {
  children: PropTypes.node,
  color: PropTypes.oneOf([
    'default',
    'primary',
    'secondary',
    'info',
    'success',
    'warning',
    'error'
  ]),
  variant: PropTypes.oneOf(['available', 'unavailable'])
};
