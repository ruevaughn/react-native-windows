import React, { Component } from 'react'
import styles from './styles'
import {
  Text,
  View,
} from 'react-native';

export default class MainPage extends Component {

  render() {
    return (
      <View style={styles.content}>
        <Text style={styles.title}>Main Page</Text>
      </View>
    )
  }

}
