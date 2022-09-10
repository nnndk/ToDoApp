import React from 'react';
import { Note } from "./Note.jsx"

export const Main = (props) => {
    const [users, setUsers] = React.useState([])

    const fetchData = async () => {
        console.log("fetch12")
        const axios = require("axios")
        let data = await axios
            .get("api/User")
            .then(res => res.data)
            .catch((error) => {})
        console.log(data)
        data = data === undefined ? [] : data
        console.log(data)

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

    const showAuthStatus = async () => {
        const t1 = await isAuth
        const t2 = await props.isAuth
        const t3 = await getIsAuth()
        console.log(`main 1 - ${t1}`)
        console.log(`main 2 - ${t2}`)
        console.log(`main 3 - ${t3}`)
    }

    return (
        <div className="d-flex flex-wrap">
            <button onClick={showAuthStatus}>is auth?</button>
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