import React from 'react';
import { Auth } from "./Auth.jsx"
import { LogInModal } from "./Modals/LogInModal.jsx"
import "./../main.css"

export const NavMenu = (props) => {
    console.log(props.isAuth)
    const [isAuth, setIsAuth] = React.useState(props.isAuth)
    const [showAuthModal, setShowAuthModal] = React.useState(false);
    const handleCloseAuthModal = () => setShowAuthModal(false);
    const handleShowAuthModal = () => setShowAuthModal(true);

    const handleLogIn = async (event) => {
        handleShowAuthModal()
    }

    const auth = () => {
        props.setIsAuth(true)
        setIsAuth(props.isAuth)
    }

    const handleLogOut = async (event) => {
        const axios = require("axios")
        await axios.get("api/user/logout")
        console.log(`1 - ${props.isAuth}`)
        props.setIsAuth(false)
        console.log(`2 - ${props.isAuth}`)
        setIsAuth(props.isAuth)
    }

    const showAuthStatus = async () => {
        console.log(await props.isAuth)
    }

    return (
        <>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container-fluid">
                    <a className="navbar-brand" href="/">ToDoApp</a>
                    <a className="nav-link login-link" aria-current="page" onClick={handleLogOut}>
                        <span>
                            <i className="bi bi-door-open-fill"></i>
                            <b>Log Out</b>
                        </span>
                    </a>
                    <a className="nav-link login-link" aria-current="page" onClick={handleLogIn}>
                        <span>
                            <i className="bi bi-person-fill mx-1 login-icon"></i>
                            <b>Log In</b>
                        </span>
                    </a>
                    <button onClick={showAuthStatus}>isAuth?</button>
                    {/*isAuth ?
                        <a className="nav-link login-link" aria-current="page" onClick={handleLogOut}>
                            <span>
                                <i className="bi bi-door-open-fill"></i>
                                <b>Log Out</b>
                            </span>
                        </a> :
                        <a className="nav-link login-link" aria-current="page" onClick={handleLogIn}>
                            <span>
                                <i className="bi bi-person-fill mx-1 login-icon"></i>
                                <b>Log In</b>
                            </span>
                        </a>
                    */}
                </div>
            </nav>

            <Auth show={showAuthModal} handleClose={handleCloseAuthModal} auth={auth} />
        </>
    );
}