const account = JSON.parse(sessionStorage.getItem('user')) || {
  displayName: '',
  email: '',
  photoURL: '/static/mock-images/avatars/avatar_default.jpg',
  accessToken: '',
  loggedIn: false
};

export default account;
