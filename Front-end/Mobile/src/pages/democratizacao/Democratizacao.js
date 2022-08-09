import react from "react";
import { View, Text, StyleSheet } from 'react-native';

export default function Democratizacao() {
    return (
        <View style={styles.container}>
            <Text style={styles.text}>Democratizacao</Text>
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
    text: {
        fontSize: 25,
        fontWeight: 'bold'
    }
});