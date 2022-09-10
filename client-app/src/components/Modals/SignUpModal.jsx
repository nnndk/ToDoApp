import React from "react"
import Modal from 'react-bootstrap/Modal';
import "./modalStyles.css"

export const SignUpModal = (props) => {
    const [authData, setAuthData] = React.useState({
        email: "",
        login: "",
        password: "",
        confirmPassword: ""
    })

    const [showPassword, setShowPassword] = React.useState(false)

    const handleChange = (event) => {
        const { name, value } = event.target

        setAuthData(prevAuthData => {
            return {
                ...prevAuthData,
                [name]: value
            }
        })
    }

    const signUp = async (event) => {
        event.preventDefault()
        const axios = require("axios")
        const result = await axios.post("api/User/register", {
            email: authData.email,
            login: authData.login,
            password: authData.password
        })

        props.auth()
        props.handleClose()
    }

    return (
        <Modal show={props.show} onHide={props.handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Sign Up</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <form onSubmit={signUp}>
                    <div className="mb-3 modal-form-item">
                        <label className="form-label">Email</label>
                        <input className="form-control" type="email" name="email"
                            value={authData.email} onChange={handleChange} id="email" required />
                    </div>
                    <div className="mb-3 modal-form-item">
                        <label className="form-label">Login</label>
                        <input className="form-control" type="text" name="login"
                            value={authData.login} onChange={handleChange} id="login" required />
                    </div>
                    <div className="mb-3 modal-form-item">
                        <label className="form-label">Password</label>
                        <span className="input-group">
                            <input className="form-control mx-2" id="password" type={showPassword ? "text" : "password"}
                                name="password" value={authData.password} onChange={handleChange} required />
                            <i onClick={() => setShowPassword(prev => !prev)}
                                className={showPassword ? "show-pass-icon bi bi-eye-fill" : "show-pass-icon bi bi-eye-slash-fill"} /> 
                        </span>
                    </div>
                    <div className="modal-footer">
                        <a className="nav-link active sign-up-link me-auto" aria-current="page"
                            onClick={() => props.setModal("logIn")}>
                            <b>Log In</b>
                        </a>
                        <button type="button" className="btn btn-secondary" onClick={props.handleClose}>
                            Close
                        </button>
                        <button type="submit" className="btn btn-primary">
                            Sign Up
                        </button>
                    </div>
                </form>
            </Modal.Body>
        </Modal>
    )
}