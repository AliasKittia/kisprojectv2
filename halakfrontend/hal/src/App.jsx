import React, { useState, useEffect } from 'react';
import axios from 'axios';

function App() {
  const [halak, setHalak] = useState([]);
  const [tavakNev, setTavakNev] = useState('');
  const [horgaszokFogasok, setHorgaszokFogasok] = useState([]);
  const [legnagyobbHal, setLegnagyobbHal] = useState(null);

  useEffect(() => {
    if (tavakNev) {
      axios.get(`http://localhost:5071/api/halak/byTavakNev/${tavakNev}`)
        .then(response => setHalak(response.data))
        .catch(error => console.error('Error fetching data:', error));
    }
  }, [tavakNev]);

  useEffect(() => {
    axios.get('http://localhost:5071/api/fogasok/horgaszok')
      .then(response => setHorgaszokFogasok(response.data))
      .catch(error => console.error('Error fetching data:', error));

    axios.get('http://localhost:5071/api/halak/legnagyobb')
      .then(response => setLegnagyobbHal(response.data))
      .catch(error => console.error('Error fetching data:', error));
  }, []);

  return (
    <div>
      <h1>Halak Listája</h1>
      <input
        type="text"
        placeholder="Tavak neve"
        value={tavakNev}
        onChange={e => setTavakNev(e.target.value)}
      />
      <table>
        <thead>
          <tr>
            <th>Név</th>
            <th>Faj</th>
            <th>Tó neve</th>
          </tr>
        </thead>
        <tbody>
          {halak.map(hal => (
            <tr key={hal.id}>
              <td>{hal.nev}</td>
              <td>{hal.faj}</td>
              <td>{hal.tavakNev}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <h1>Horgászok Fogásai</h1>
      <table>
        <thead>
          <tr>
            <th>Horgász neve</th>
            <th>Hal neve</th>
            <th>Fogás dátuma</th>
          </tr>
        </thead>
        <tbody>
          {horgaszokFogasok.map(fogas => (
            <tr key={`${fogas.HorgaszNev}-${fogas.HalNev}-${fogas.Datum}`}>
              <td>{fogas.HorgaszNev}</td>
              <td>{fogas.HalNev}</td>
              <td>{new Date(fogas.Datum).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <h1>Legnagyobb Hal</h1>
      {legnagyobbHal && (
        <div>
          <p>Név: {legnagyobbHal.Nev}</p>
          <p>Méret: {legnagyobbHal.MeretCm} cm</p>
        </div>
      )}
    </div>
  );
}

export default App;