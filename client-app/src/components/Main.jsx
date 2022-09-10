import React from 'react';
import { Note } from "./Note.jsx"

export const Main = (props) => {
    const [users, setUsers] = React.useState([])

    const fetchData = async () => {
        const axios = require("axios")
        let data = await axios
            .get("api/User")
            .then(res => res.data)
            .catch((error) => {})

        data = data === undefined ? [] : data
        setUsers(data)
    }

    const getIsAuth = async () => {
        const t = await props.getIsAuth()
        return t
    }

    const [isAuth, setIsAuth] = React.useState(getIsAuth())

    React.useEffect(() => {
        const getResult = async () => {
            const res = await props.isAuth
            return res
        }

        setIsAuth(getResult())
    }, [props.isAuth])

    React.useEffect(() => {
        fetchData()
    }, [isAuth])

    return (
        <div className="d-flex flex-wrap">
            <Note />
            {users.map(user =>
                <p key={user.id}>
                    {user.id}<br />
                    {user.login}<br />
                    {user.email}<br />
                    {user.password}<br />
                    <br />
                </p>)}
        </div>
    )
}