import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { View, Text, ScrollView } from 'react-native';
import styles from './styles'

export default class LogArea extends Component {
  static propTypes = {
    content: PropTypes.string
  }

  render() {
    return (
      <View style={styles.content}>
        <ScrollView style={styles.log}>
          <Text>{this.props.content}</Text>
        </ScrollView>
      </View>
    )
  }
}