import React from "react"

import { LogInModal } from "./Modals/LogInModal.jsx"
import { SignUpModal } from "./Modals/SignUpModal.jsx"

export const Auth = (props) => {
    const [modal, setModal] = React.useState("logIn")
    const handleClose = () => {
        props.handleClose()
        setModal("logIn")
    }

    switch (modal) {
        case "logIn":
            return <LogInModal show={props.show} handleClose={handleClose} setModal={setModal} auth={props.auth} />
        case "signUp":
            return <SignUpModal show={props.show} handleClose={handleClose} setModal={setModal} auth={props.auth}  />
        default:
            <></>
    }
}
