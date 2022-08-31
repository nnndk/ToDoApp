import React from 'react';
import { Auth } from "./Auth.jsx"
import { LogInModal } from "./Modals/LogInModal.jsx"
import "./../main.css"

export const NavMenu = () => {
    const [showAuthModal, setShowAuthModal] = React.useState(false);
    const handleCloseAuthModal = () => setShowAuthModal(false);
    const handleShowAuthModal = () => setShowAuthModal(true);

    const handleLogIn = async (event) => {
        handleShowAuthModal()
    }

    return (
        <>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container-fluid">
                    <a className="navbar-brand" href="/">ToDoApp</a>
                    <a className="nav-link login-link" aria-current="page" onClick={handleLogIn}>
                        <span>
                            <i className="bi bi-person-fill mx-1 login-icon"></i>
                            <b>Log In</b>
                        </span>
                    </a>
                </div>
            </nav>

            <Auth show={showAuthModal} handleClose={handleCloseAuthModal} />
        </>
    );
}