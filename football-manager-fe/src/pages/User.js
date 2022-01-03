import { useEffect, useState } from 'react';
import { filter, sample } from 'lodash';
import faker from 'faker';
// material
import axios from 'axios';
// utils
// components
import PlayersData from '../components/_dashboard/app/PlayersData';

// ----------------------------------------------------------------------

export default function User() {
  const [data, setData] = useState(null);
  const url = '/api/player/Players';

  useEffect(() => {
    axios(url).then((response) => {
      setData(response.data);
    });
  }, []);
  return <PlayersData data={data} />;
}
