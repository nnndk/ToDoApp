import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu.jsx';

export const Layout = (props) => {
  return (
    <>
      <NavMenu />
      <Container>
        {props.children}
      </Container>
    </>
  );
}
