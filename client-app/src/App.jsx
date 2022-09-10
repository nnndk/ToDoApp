import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { Layout } from './components/Layout.jsx';
import { Main } from "./components/Main.jsx"

export const App = () => {
  const getAuthStatus = async () => {
    const axios = require("axios")
    return await axios
      .get("api/user/IsAuth")
      .then(res => res.data)
  }

  const [isAuth, setIsAuth] = React.useState(false)

  const getIsAuth = async () => {
    return await isAuth
  }

  React.useEffect(() => {
    const getAuthStatusResult = async () => {
      const result = await getAuthStatus()
      console.log(`abc - ${result}`)

      return result
    }
    
    const authStatus = getAuthStatusResult()
    setIsAuth(authStatus)

    console.log(isAuth)
  }, [])

  return (
    <Layout isAuth={isAuth} getIsAuth={getIsAuth} setIsAuth={setIsAuth} getAuthStatus={getAuthStatus}>
      <Routes>
        <Route exact path="/" element={<Main isAuth={isAuth} getIsAuth={getIsAuth} />} />
      </Routes>
    </Layout>
  );
}
