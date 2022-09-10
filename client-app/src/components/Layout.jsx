import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu.jsx';

export const Layout = (props) => {
  const showAuthStatus = async () => {
    const t = await props.getAuthStatus()
    console.log(t)
  }
  
  return (
    <>
      <NavMenu isAuth={props.getIsAuth()} setIsAuth={props.setIsAuth} />
      <button onClick={showAuthStatus}>is auth?</button>
      <Container>
        {props.children} 
      </Container>
    </>
  );
}
