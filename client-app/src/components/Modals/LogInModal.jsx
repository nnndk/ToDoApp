import React from "react"
import Modal from 'react-bootstrap/Modal';
import "./modalStyles.css"

export const LogInModal = (props) => {
    const [authData, setAuthData] = React.useState({
        login: "",
        password: ""
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

    const logIn = async (event) => {
        event.preventDefault()
        /*let currDate = new Date()
        
        const result = await fetch('TestItems', {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                someText: item.someText,
                doubleNumber: parseFloat(item.doubleNumber),
                boolValue: item.boolValue,
                datetimeTest: new Date(item.datetimeTest),
                lastChange: `${currDate.getFullYear().toString().padStart(4, "0")}-` +
                    `${currDate.getMonth().toString().padStart(2, "0")}-` +
                    `${currDate.getDate().toString().padStart(2, "0")}T` +
                    `${currDate.getHours().toString().padStart(2, "0")}:` +
                    `${currDate.getMinutes().toString().padStart(2, "0")}:` +
                    `${currDate.getSeconds().toString().padStart(2, "0")}`
            })
        })

        props.fetchData()*/
        props.handleClose()
    }

    return (
        <Modal show={props.show} onHide={props.handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Log In</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <form onSubmit={logIn}>
                    <div className="mb-3 modal-form-item">
                        <label for="login" class="form-label">Login</label>
                        <input className="form-control" type="text" name="login"
                            value={authData.login} onChange={handleChange} id="login" required />
                    </div>
                    <div className="mb-3 modal-form-item">
                        <label for="password" class="form-label">Password</label>
                        <div className="input-group">
                            <input className="form-control mx-2" id="password" type={showPassword ? "text" : "password"}
                                name="password" value={authData.password} onChange={handleChange} required />
                            <i onClick={() => setShowPassword(prev => !prev)}
                                className={showPassword ? "show-pass-icon bi bi-eye-fill" : "show-pass-icon bi bi-eye-slash-fill"} />
                        </div>
                    </div>
                    <div className="modal-footer">
                        <a className="nav-link active sign-up-link me-auto" aria-current="page"
                            onClick={() => props.setModal("signUp")}>
                            <b>Sign Up</b>
                        </a>
                        <button type="button" className="btn btn-secondary" onClick={props.handleClose}>
                            Close
                        </button>
                        <button type="submit" className="btn btn-primary">
                            Log In
                        </button>
                    </div>
                </form>
            </Modal.Body>
        </Modal>
    )
}