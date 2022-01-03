export const useTeamsData = () => {
    return async () => {
        const response = await fetch("")
        const json = response.ok ? await response.json() : {};

        return json;
    }
}