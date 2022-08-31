import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { Layout } from './components/Layout.jsx';
import { Main } from "./components/Main.jsx"

export const App = () => {
  return (
    <Layout>
      <Routes>
        <Route exact path="/" element={<Main />} />
      </Routes>
    </Layout>
  );
}
