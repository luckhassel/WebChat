export interface UserLogged {
    user: {
        id: number,
        username: string,
        password: string,
        role: string
    },
    token: string
}
