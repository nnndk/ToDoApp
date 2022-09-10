import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu.jsx';

export const Layout = (props) => {
  
  return (
    <>
      <NavMenu isAuth={props.getIsAuth()} setIsAuth={props.setIsAuth} />
      <Container>
        {props.children} 
      </Container>
    </>
  );
}
