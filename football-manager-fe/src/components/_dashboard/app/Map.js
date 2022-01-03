import { ZoomableGroup, ComposableMap, Geographies, Geography } from 'react-simple-maps';
import ReactTooltip from 'react-tooltip';
import React, { useState, useEffect } from 'react';
import Card from '@mui/material/Card';
import clm from 'country-locale-map';

export default function Map({ data }) {
  const geoUrl =
    'https://raw.githubusercontent.com/zcreativelabs/react-simple-maps/master/topojson-maps/world-110m.json';

  const [content, setContent] = useState('');

  return (
    <Card>
      <ComposableMap data-tip="" projectionConfig={{ scale: 200 }}>
        <ZoomableGroup>
          <Geographies geography={geoUrl}>
            {({ geographies }) =>
              geographies.map((geo) => (
                <Geography
                  key={geo.rsmKey}
                  geography={geo}
                  onMouseEnter={() => {
                    const { NAME } = geo.properties;
                    let ALPHA = clm.getAlpha2ByName(NAME);
                    if (data !== null) {
                      if (data[ALPHA] === undefined) data[ALPHA] = 0;
                      if (ALPHA === 'US') ALPHA = 'EN';
                      setContent(`${NAME}  -  ${data[ALPHA]}`);
                    }
                  }}
                  onMouseLeave={() => {
                    setContent('');
                  }}
                  style={{
                    default: {
                      fill: '#D6D6DA',
                      outline: 'none'
                    },
                    hover: {
                      fill: '#4caf50',
                      outline: 'none'
                    },
                    pressed: {
                      fill: '#4caf50',
                      outline: 'none'
                    }
                  }}
                />
              ))
            }
          </Geographies>
        </ZoomableGroup>
      </ComposableMap>
      <ReactTooltip>{content}</ReactTooltip>
    </Card>
  );
}
