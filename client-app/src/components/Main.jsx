import React from 'react';
import { Note } from "./Note.jsx"

export const Main = () => {
    const [users, setUsers] = React.useState([])

    const fetchData = async () => {
        const axios = require("axios")
        const data = await axios
            .get("api/User")
            .then(res => res.data)

        setUsers(data)
    }

    React.useEffect(() => {
        fetchData()
    }, [])

    return (
        <div className="d-flex flex-wrap">
            <Note />
            {users.map(user =>
                <p>
                    {user.id}<br />
                    {user.login}<br />
                    {user.email}<br />
                    {user.password}<br />
                    <br />
                </p>)}
        </div>
    )
}