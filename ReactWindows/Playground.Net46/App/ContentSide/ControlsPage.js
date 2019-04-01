import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  View
} from 'react-native'
import styles from './styles'
import TextInputTest from './TextInputTest'
import ImageTest from './ImageTest'

export default class ControlsPage extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  constructor(props) {
    super(props)
  }

  render() {
    return (
      <View style={styles.content}>
        <Text testID='Text_Controls' style={styles.title}>Controls</Text>
        <TextInputTest style={styles.item} logger={this.props.logger} />
        <ImageTest style={styles.item} logger={this.props.logger} />
      </View>
    )
  }
}